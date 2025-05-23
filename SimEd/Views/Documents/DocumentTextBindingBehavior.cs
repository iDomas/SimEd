using Avalonia;
using Avalonia.Xaml.Interactivity;
using AvaloniaEdit;

namespace SimEd.Views.Documents;

public class DocumentTextBindingBehavior : Behavior<TextEditor>
{
    private TextEditor? _textEditor;

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<DocumentTextBindingBehavior, string>(nameof(Text));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is TextEditor textEditor)
        {
            _textEditor = textEditor;
            _textEditor.TextChanged += TextChanged;
            this.GetObservable(TextProperty).Subscribe(new OnTextPropertyChanged(_textEditor));
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_textEditor != null)
        {
            _textEditor.TextChanged -= TextChanged;
        }
    }

    private void TextChanged(object sender, EventArgs eventArgs)
    {
        if (_textEditor is not null && _textEditor.Document != null)
        {
            Text = _textEditor.Document.Text;
        }
    }

    private class OnTextPropertyChanged(TextEditor textEditor) : IObserver<string>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(string? text)
        {
            if (textEditor.Document is null || text is null)
            {
                return;
            }
            int caretOffset = textEditor.CaretOffset;
            textEditor.Document.Text = text;
            textEditor.CaretOffset = caretOffset;
        }
    }
}