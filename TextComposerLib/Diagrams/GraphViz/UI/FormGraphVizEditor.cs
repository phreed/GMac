﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TextComposerLib.Diagrams.GraphViz.Dot;

namespace TextComposerLib.Diagrams.GraphViz.UI
{
    public partial class FormGraphVizEditor : Form
    {
        //private readonly DotGraph _graph;


        public FormGraphVizEditor(DotGraph graph)
        {
            InitializeComponent();

            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            //_graph = graph;

            textBoxBinFolder.Text = StringUtils.Settings["graphvizFolder"];

            textBoxDotSource.Text = graph.GenerateDotCode();

            comboBoxRenderMethod.Items.Add(GvRenderer.Dot);
            comboBoxRenderMethod.Items.Add(GvRenderer.Neato);
            comboBoxRenderMethod.Items.Add(GvRenderer.Circo);
            comboBoxRenderMethod.Items.Add(GvRenderer.TwoPi);
            comboBoxRenderMethod.Items.Add(GvRenderer.Fdp);
            comboBoxRenderMethod.Items.Add(GvRenderer.SFdp);
            comboBoxRenderMethod.Items.Add(GvRenderer.PatchWork);
            comboBoxRenderMethod.Items.Add(GvRenderer.OSage);

            comboBoxRenderMethod.SelectedIndex = 0;

            comboBoxOutputFormat.Items.Add(GvOutputFormat.Bmp);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.CMap);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.CMapX);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.CMapXNp);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Canon);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Dot);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Emf);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.EmfPlus);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Eps);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Fig);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Gd);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Gd2);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Gif);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Gv);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Imap);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.ImapNp);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.IsMap);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Jpe);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Jpeg);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Jpg);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.MetaFile);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Pdf);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Pic);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Plain);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.PlainExt);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Png);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Pov);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Ps);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Ps2);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Svg);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Svgz);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Tk);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Tif);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Tiff);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Vml);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Vmlz);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.Vrml);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.XDot);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.XDot12);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.XDot14);
            comboBoxOutputFormat.Items.Add(GvOutputFormat.WBmp);

            comboBoxOutputFormat.SelectedIndex = 0;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RenderDotCode(GvRenderer graphRenderer, GvOutputFormat outFormat)
        {
            var filePath = "";

            graphRenderer.GraphVizBinFolder = textBoxBinFolder.Text;

            if (checkBoxSaveToFile.Checked)
            {
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Title = @"Save Rendering Result to File";

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    filePath = saveFileDialog.FileName;
            }

            var imageOutput = outFormat as GvImageOutputFormat;

            if (imageOutput != null)
            {
                var image = String.IsNullOrEmpty(filePath)
                    ? graphRenderer.RenderToImage(textBoxDotSource.Text, imageOutput)
                    : graphRenderer.RenderToImageAndFile(textBoxDotSource.Text, imageOutput, filePath);

                if (image == null) return;

                var imageForm = new FormImageDisplay();

                imageForm.SetImage(image);

                imageForm.Show(this);

                return;
            }

            var textOutput = outFormat as GvTextOutputFormat;

            if (textOutput != null)
            {
                var text = String.IsNullOrEmpty(filePath)
                    ? graphRenderer.RenderToText(textBoxDotSource.Text, textOutput)
                    : graphRenderer.RenderToTextAndFile(textBoxDotSource.Text, textOutput, filePath);

                if (string.IsNullOrEmpty(text)) return;

                var textForm = new FormTextDisplay {textBoxText = {Text = text}};

                textForm.Show(this);

                return;
            }

            if (String.IsNullOrEmpty(filePath))
            {
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.Title = @"Save Rendering Result to File";

                if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel)
                    return;

                filePath = saveFileDialog.FileName;
            }

            graphRenderer.RenderToFile(textBoxDotSource.Text, outFormat, filePath);
        }

        private void buttonRender_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxDotSource.Text.Trim()))
            {
                MessageBox.Show(@"No dot source to render!");

                return;
            }

            var graphRenderer = comboBoxRenderMethod.SelectedItem as GvRenderer;

            if (graphRenderer == null)
            {
                MessageBox.Show(@"No renderer selected!");

                return;
            }

            var time = DateTime.Now;

            graphRenderer.VerboseOutput = checkBoxVerbose.Checked;

            RenderDotCode(
                graphRenderer, 
                comboBoxOutputFormat.SelectedItem as GvOutputFormat
                );

            var elapsedTime = DateTime.Now - time;

            var s = new StringBuilder();

            s.Append("Rendering took: ")
                .AppendLine(elapsedTime.ToString("G"))
                .AppendLine()
                .Append(graphRenderer.RenderingErrorsMessage);

            textBoxRendererOutput.Text = s.ToString();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "exe";
            openFileDialog.FileName = "dot.exe";
            openFileDialog.Filter = @"Dot.exe|dot.exe";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = @"Select GraphViz Bin Folder";

            if (openFileDialog.ShowDialog(this) == DialogResult.Cancel)
                return;

            textBoxBinFolder.Text = Path.GetDirectoryName(openFileDialog.FileName);

            //textBoxRendererOutput.Text = "";

            //_graph = DotGraph.Directed();

            //_graph.AddNode("A").SetShape(DotNodeShape.Septagon);
            //_graph.AddNode("B").SetShape(DotNodeShape.Box3D);
            //_graph.AddNode("C").SetShape(DotNodeShape.Circle);

            //_graph.AddEdge("A", "B");
            //_graph.AddEdge("B", "C");
            //_graph.AddEdge("C", "A");

            //_graph.AddNodeDefaults().SetShape(DotNodeShape.Component);
            //_graph.AddCluster("1").AddEdge("1", "2", "3", "4");

            //_graph.AddNode("C").SetShape(DotNodeShape.Diamond);

            //textBoxDotSource.Text = _graph.GenerateDotCode();
        }

        private void textBoxBinFolder_TextChanged(object sender, EventArgs e)
        {
            //if (!File.Exists(Path.Combine(textBoxBinFolder.Text, "dot.exe")))
            //    return;

            StringUtils.Settings["graphvizFolder"] = textBoxBinFolder.Text;
            StringUtils.Settings.ChainToFile();
        }
    }
}
