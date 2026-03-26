using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace Calculator.IntegrationTests;

public class MainWindowIntegrationTests
{
    [Fact]
    public void StartupState_ShouldShowZeroAndReady()
    {
        RunInSta(() =>
        {
            MainWindow window = CreateWindow();

            Assert.Equal("0", Display(window).Text);
            Assert.Equal("Ready", Status(window).Text);

            window.Close();
        });
    }

    [Fact]
    public void NumberAndDecimalEntry_ShouldBuildExpectedDisplay()
    {
        RunInSta(() =>
        {
            MainWindow window = CreateWindow();

            ClickByTag(window, "1");
            ClickByContent(window, ".");
            ClickByTag(window, "2");
            ClickByContent(window, ".");
            ClickByTag(window, "3");

            Assert.Equal("1.23", Display(window).Text);
            Assert.Equal("Ready", Status(window).Text);

            window.Close();
        });
    }

    [Fact]
    public void BinaryWorkflow_ShouldUpdateDisplayAndStatusOnEquals()
    {
        RunInSta(() =>
        {
            MainWindow window = CreateWindow();

            ClickByTag(window, "7");
            ClickByTag(window, "+");
            ClickByTag(window, "8");
            ClickByContent(window, "=");

            Assert.Equal("15", Display(window).Text);
            Assert.Equal("7 + 8 = 15", Status(window).Text);

            window.Close();
        });
    }

    [Fact]
    public void ClearAll_ShouldResetPendingOperation()
    {
        RunInSta(() =>
        {
            MainWindow window = CreateWindow();

            ClickByTag(window, "9");
            ClickByTag(window, "+");
            ClickByTag(window, "1");
            ClickByTag(window, "CA");
            ClickByTag(window, "2");
            ClickByContent(window, "=");

            Assert.Equal("2", Display(window).Text);
            Assert.Equal("Ready", Status(window).Text);

            window.Close();
        });
    }

    [Fact]
    public void MemoryWorkflow_ShouldSupportAddRecallAndClear()
    {
        RunInSta(() =>
        {
            MainWindow window = CreateWindow();

            ClickByTag(window, "5");
            ClickByTag(window, "M+");
            ClickByTag(window, "CA");
            ClickByTag(window, "MR");
            Assert.Equal("5", Display(window).Text);

            ClickByTag(window, "MC");
            ClickByTag(window, "MR");

            Assert.Equal("0", Display(window).Text);
            Assert.Equal("Memory recalled: 0", Status(window).Text);

            window.Close();
        });
    }

    private static MainWindow CreateWindow()
    {
        MainWindow window = new();
        window.ShowInTaskbar = false;
        window.WindowStyle = WindowStyle.None;
        return window;
    }

    private static TextBox Display(MainWindow window) =>
        (TextBox)(window.FindName("DisplayText") ?? throw new InvalidOperationException("DisplayText not found."));

    private static TextBlock Status(MainWindow window) =>
        (TextBlock)(window.FindName("StatusText") ?? throw new InvalidOperationException("StatusText not found."));

    private static void ClickByTag(MainWindow window, string tag)
    {
        Button button = FindButtons(window)
            .FirstOrDefault(candidate => string.Equals(candidate.Tag?.ToString(), tag, StringComparison.Ordinal))
            ?? throw new InvalidOperationException($"Button with Tag '{tag}' not found.");

        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
    }

    private static void ClickByContent(MainWindow window, string content)
    {
        Button button = FindButtons(window)
            .FirstOrDefault(candidate => string.Equals(candidate.Content?.ToString(), content, StringComparison.Ordinal))
            ?? throw new InvalidOperationException($"Button with Content '{content}' not found.");

        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
    }

    private static IEnumerable<Button> FindButtons(DependencyObject root)
    {
        foreach (object child in LogicalTreeHelper.GetChildren(root))
        {
            if (child is Button button)
            {
                yield return button;
            }

            if (child is DependencyObject childDependencyObject)
            {
                foreach (Button descendant in FindButtons(childDependencyObject))
                {
                    yield return descendant;
                }
            }
        }
    }

    private static void RunInSta(Action testAction)
    {
        Exception? captured = null;
        ManualResetEventSlim completed = new(false);

        Thread thread = new(() =>
        {
            try
            {
                testAction();
            }
            catch (Exception ex)
            {
                captured = ex;
            }
            finally
            {
                completed.Set();
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        completed.Wait();

        if (captured is not null)
        {
            if (captured is TargetInvocationException target && target.InnerException is not null)
            {
                throw target.InnerException;
            }

            throw captured;
        }
    }
}
