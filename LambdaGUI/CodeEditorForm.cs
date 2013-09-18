using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SchemeCore;
using SchemeCore.objects;
using ICSharpCode;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;

namespace LambdaGUI
{
    public partial class CodeEditorForm :Form
    {
        ITextEditorProperties _editorSettings;
        public CodeEditorForm()
        {
            InitializeComponent();
            
           // codeWindow.


        }

        private void btnEvaluate_Click( object sender, EventArgs e )
        {
            var reader = new SchemeReader();
            var evaluator = new SchemeEvaluator();
            var ast = reader.parseString( codeWindow.Text );
            var res = evaluator.evaluate(ast);

           // resultWindow.
            foreach( SchemeObject obj in res )
            {
                resultWindow.AppendText(obj.ToString() + "\n");
            }

            var document = codeWindow.Document;
            var location = new ICSharpCode.TextEditor.TextLocation( 1, 1 );

            Bookmark book = new Bookmark( document, location );
            document.BookmarkManager.AddMark( book );
           

            var margin = codeWindow.Margin;

            

            var marker = new TextMarker(1, 1, TextMarkerType.SolidBlock);
            document.MarkerStrategy.AddMarker( marker );
            codeWindow.Update();
            //document.BookmarkManager.Factory.CreateBookmark( codeWindow.Document, location );
           
        }

        private TextEditorControl AddNewTextEditor( string title )
        {
            var tab = new TabPage( title );
            var editor = new TextEditorControl();
            editor.Dock = System.Windows.Forms.DockStyle.Fill;
            editor.IsReadOnly = false;
            editor.Document.DocumentChanged +=
                new DocumentEventHandler( ( sender, e ) => { SetModifiedFlag( editor, true ); } );
            // When a tab page gets the focus, move the focus to the editor control
            // instead when it gets the Enter (focus) event. I use BeginInvoke 
            // because changing the focus directly in the Enter handler doesn't 
            // work.
            tab.Enter +=
                new EventHandler( ( sender, e ) =>
                {
                    var page = ( (TabPage) sender );
                    page.BeginInvoke( new Action<TabPage>( p => p.Controls[0].Focus() ), page );
                } );
            tab.Controls.Add( editor );

            fileTabs.Controls.Add( tab );

            if( _editorSettings == null )
            {
                _editorSettings = editor.TextEditorProperties;
                OnSettingsChanged();
            }
            else
                editor.TextEditorProperties = _editorSettings;
            return editor;
        }
        private bool IsModified( TextEditorControl editor )
        {
            // TextEditorControl doesn't seem to contain its own 'modified' flag, so 
            // instead we'll treat the "*" on the filename as the modified flag.
            return editor.Parent.Text.EndsWith( "*" );
        }
        private void SetModifiedFlag( TextEditorControl editor, bool flag )
        {
            if( IsModified( editor ) != flag )
            {
                var p = editor.Parent;
                if( IsModified( editor ) )
                    p.Text = p.Text.Substring( 0, p.Text.Length - 1 );
                else
                    p.Text += "*";
            }
        }

        private void OnSettingsChanged()
        {
        //    menuShowSpacesTabs.Checked = _editorSettings.ShowSpaces;
         //   menuShowNewlines.Checked = _editorSettings.ShowEOLMarker;
          //  menuHighlightCurrentRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
           // menuBracketMatchingStyle.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
           // menuEnableVirtualSpace.Checked = _editorSettings.AllowCaretBeyondEOL;
           // menuShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
        }

        private void menuFileNew_Click( object sender, EventArgs e )
        {
            AddNewTextEditor( "New file" );
        }

        private void codeWindow_Load( object sender, EventArgs e )
        {

        }

        private void codeWindow_Load_1( object sender, EventArgs e )
        {

        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {

        }
    }
}
