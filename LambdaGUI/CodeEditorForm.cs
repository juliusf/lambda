using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SchemeCore;

namespace LambdaGUI
{
    public partial class CodeEditorForm :Form
    {
        public CodeEditorForm()
        {
            InitializeComponent();
        }

        private void btnEvaluate_Click( object sender, EventArgs e )
        {
            var reader = new SchemeReader();
            var evaluator = new SchemeEvaluator();
            var ast = reader.parseString( codeWindow.Text );

            resultWindow.Text = resultWindow.Text + evaluator.evaluate( ast );
        }
    }
}
