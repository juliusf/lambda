using System;
using System.IO;
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

using SchemeCore.helper;

namespace LambdaGUI
{
    public partial class CodeEditorForm :Form
    {
        ITextEditorProperties _editorSettings;
        private TextEditorControl ActiveEditor
        {
            get
            {
                if( fileTabs.TabPages.Count == 0 ) return null;
                return (TextEditorControl) fileTabs.SelectedTab.Controls[0];
            }
        }
        public CodeEditorForm()
        {
            InitializeComponent();
            
           // codeWindow.


        }

        private void btnEvaluate_Click( object sender, EventArgs e )
        {
            evaluate();
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

        private void RemoveTextEditor( TextEditorControl editor )
        {
            ( (TabControl) editor.Parent.Parent ).Controls.Remove( editor.Parent );
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
        private void OpenFiles( string[] fns )
        {
            // Close default untitled document if it is still empty
            if( fileTabs.TabPages.Count == 1
                && ActiveEditor.Document.TextLength == 0
                && string.IsNullOrEmpty( ActiveEditor.FileName ) )
                RemoveTextEditor( ActiveEditor );

            // Open file(s)
            foreach( string fn in fns )
            {
                var editor = AddNewTextEditor( Path.GetFileName( fn ) );
                try
                {
                    editor.LoadFile( fn );
                    // Modified flag is set during loading because the document 
                    // "changes" (from nothing to something). So, clear it again.
                    SetModifiedFlag( editor, false );
                }
                catch( Exception ex )
                {
                    MessageBox.Show( ex.Message, ex.GetType().Name );
                    RemoveTextEditor( editor );
                    return;
                }

                // ICSharpCode.TextEditor doesn't have any built-in code folding
                // strategies, so I've included a simple one. Apparently, the
                // foldings are not updated automatically, so in this demo the user
                // cannot add or remove folding regions after loading the file.
                editor.Document.FoldingManager.FoldingStrategy = new RegionFoldingStrategy();
                editor.Document.FoldingManager.UpdateFoldings( null, null );
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

        private void menuFileOpen_Click( object sender, EventArgs e )
        {
            if( openFileDialog.ShowDialog() == DialogResult.OK )
                // Try to open chosen file
                OpenFiles( openFileDialog.FileNames );
        }

        private void evaluateToolStripMenuItem_Click( object sender, EventArgs e )
        {
            evaluate();
        }

        public void evaluate()
        {
            var reader = new SchemeReader();
            var evaluator = new SchemeEvaluator();

            var ast = reader.parseString( ActiveEditor.Text );
            var res = evaluator.evaluate( ast );

            // resultWindow.
            foreach( SchemeObject obj in res )
            {
                resultWindow.AppendText( obj.ToString() + "\n" );
            }

            var document = codeWindow.Document;
            var location = new ICSharpCode.TextEditor.TextLocation( 1, 1 );

            Bookmark book = new Bookmark( document, location );
            document.BookmarkManager.AddMark( book );


            var margin = codeWindow.Margin;



            var marker = new TextMarker( 1, 1, TextMarkerType.SolidBlock );
            document.MarkerStrategy.AddMarker( marker );
            codeWindow.Update();
        }

        private void CodeEditorForm_Load( object sender, EventArgs e )
        {
            Logger.logWrite += new Logger.logWriteEventHandler( onLogWrite );
            Logger.logWriteLn += new Logger.logWriteLnEventHandler( onLogWriteLn );
        }

        private void onLogWrite( string text )
        {
            resultWindow.AppendText( text );
            resultWindow.SelectionStart = resultWindow.Text.Length;
            resultWindow.ScrollToCaret();
        }

        private void onLogWriteLn( string text )
        {
            resultWindow.AppendText( text + '\n');
            resultWindow.SelectionStart = resultWindow.Text.Length;
            resultWindow.ScrollToCaret();
        }


    }
    public class RegionFoldingStrategy :IFoldingStrategy
    {
        /// <summary>
        /// Generates the foldings for our document.
        /// </summary>
        /// <param name="document">The current document.</param>
        /// <param name="fileName">The filename of the document.</param>
        /// <param name="parseInformation">Extra parse information, not used in this sample.</param>
        /// <returns>A list of FoldMarkers.</returns>
       
        public List<FoldMarker> GenerateFoldMarkers( IDocument document, string fileName, object parseInformation )
        {
            List<FoldMarker> list = new List<FoldMarker>();

            Stack<int> startLines = new Stack<int>();

            // Create foldmarkers for the whole document, enumerate through every line.
            for( int i = 0; i < document.TotalNumberOfLines; i++ )
            {
                var seg = document.GetLineSegment( i );
                int offs, end = document.TextLength;
                char c;
                for( offs = seg.Offset; offs < end && ( ( c = document.GetCharAt( offs ) ) == ' ' || c == '\t' ); offs++ )
                { }
                if( offs == end )
                    break;
                int spaceCount = offs - seg.Offset;

                // now offs points to the first non-whitespace char on the line
                if( document.GetCharAt( offs ) == '#' )
                {
                    string text = document.GetText( offs, seg.Length - spaceCount );
                    if( text.StartsWith( "#region" ) )
                        startLines.Push( i );
                    if( text.StartsWith( "#endregion" ) && startLines.Count > 0 )
                    {
                        // Add a new FoldMarker to the list.
                        int start = startLines.Pop();
                        list.Add( new FoldMarker( document, start,
                            document.GetLineSegment( start ).Length,
                            i, spaceCount + "#endregion".Length ) );
                    }
                }
            }

            return list;
        }
    }

}
