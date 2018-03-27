using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FAM
{
    public partial class NomogrammaForm : Form
    {
        #region GLOBAL_DATA
        
        //координаты предыдущей точки для рисования правой клавишей мыши
        Point previousPointMark;        
        //для рисование прямоугольника выделения
        Point startPoint;
        Point previousPoint;
        Size ReversibleFrameSize;
        bool isDragging = false;
        //математические габариты области номограммы в виде массива
        float[] originalPictureBoxMathBorder = { 0, 0, 1, 2 };  // Xmin, Ymin, Xmax, Ymax
        float[] pictureBoxMathBorder = { 0, 0, 1, 2 };  // Xmin, Ymin, Xmax, Ymax;
        
        Nomogramma data;  //набор данных и функций для построения номограммы
        bool DrawLog = true;  //номограмма будет писать логи
        
        #endregion

        public NomogrammaForm()
        {
            InitializeComponent();
            DataTransfer.DataUpLoad = new DataTransfer.NomogrammaDataUploadEvent(DataUpload);  //обработчик события загрузки анных преобразования
        }  //инициализация формы и обработчиков событий

        //EVENTS**********************************************************************************
        private void Nomogramma_Load(object sender, EventArgs e)
        {
            //подгонка picturebox под размер формы
            pictureBox1.Width = this.ClientSize.Width;
            pictureBox1.Height = this.ClientSize.Height - statusStrip1.Height - toolStrip1.Height;
            
            //полотно для рисования
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //грузим полотно на picturebox
            pictureBox1.Image = (Image)bmp;

            data.CulcNomogramma(DrawLog);
            DrawNomogramma();
            pictureBox1.Invalidate();
        }
        private void NomogrammaHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("NomogrammaHelp.txt");
        }
        private void Returne_Click(object sender, EventArgs e)
        {
            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            pictureBoxMathBorder = originalPictureBoxMathBorder;  //установка прежнего массива границ
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //отображение координат на PictureBox
            toolStripStatusLabel1.Text = "Xm: " + Math.Round(FizToMathX(e.Location, pictureBoxMathBorder), 2);
            toolStripStatusLabel2.Text = "Ym: " + Math.Round(FizToMathY(e.Location, pictureBoxMathBorder), 2);            
            toolStripStatusLabel3.Text = "X: " + e.Location.X;
            toolStripStatusLabel4.Text = "Y: " + e.Location.Y;

            //объявление цветов для рисования
            Color backColor = pictureBox1.BackColor;
            Color black = Color.Black;
            Color red = Color.Red;
                       
            //для рисования прямоугольника выделения
            if (e.Button == MouseButtons.Left)
            {
                if (isDragging)
                {
                    ReversibleFrameDraw(backColor, startPoint, previousPoint);
                    previousPoint = e.Location;
                    ReversibleFrameDraw(black, startPoint, e.Location);                                                                        
                }
                else
                {
                    isDragging = true;
                    startPoint = e.Location;
                    previousPoint = e.Location;
                }
            }
            else
            {
                if (isDragging)
                {
                    ReversibleFrameDraw(backColor, startPoint, previousPoint);
                    pictureBoxMathBorderResize();  //изменение размера области отображения
                    DrawNomogramma();
                }
                isDragging = false;
            }

            //рисование по нажатии правой клавиши мыши
            if (e.Button == MouseButtons.Right)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    using (Pen p = new Pen(red, 1))
                    {
                        g.DrawLine(p, previousPointMark, e.Location);
                        previousPointMark = e.Location;
                    }
                }
                pictureBox1.Invalidate();
            }
            else
                previousPointMark = e.Location;                       
        }        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);

            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как ...";
            savedialog.FileName = "Nomogramma";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
                "Bitmap File(*.bmp)|*.bmp|" +
                "GIF File(*.gif)|*.gif|" +
                "JPEG File(*.jpg)|*.jpg|" +
                "TIF File(*.tif)|*.tif|" +
                "PNG File(*.png)|*.png";
            savedialog.ShowHelp = false;
            //если выбрано сохранить
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                //получаем введенное в поле новое имя файла
                string fileName = savedialog.FileName;
                //определяем тип контейнера
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                //файл сохраняется в определенном контейнере
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }

        }
        private void Nomogramma_Resize(object sender, EventArgs e)
        {            
            //подгонка picturebox под размер формы
            pictureBox1.Width = this.ClientSize.Width;
            pictureBox1.Height = this.ClientSize.Height - statusStrip1.Height - toolStrip1.Height;
            //полотно для рисования
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //грузим полотно на picturebox
            pictureBox1.Image = (Image)bmp;
        }  
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "5";
            if (InputBox("Получение нового параметра", "Введите Kp:", ref value) == DialogResult.OK)
            {                
                data.Kp = Convert.ToInt16(value);
            }
        }
        private void fullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullToolStripMenuItem.Checked = true;
            hulfToolStripMenuItem.Checked = false;
            data.TN = 0;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void hulfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hulfToolStripMenuItem.Checked = true;
            fullToolStripMenuItem.Checked = false;
            data.TN = 1;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleToolStripMenuItem.Checked = true;
            balanceGeterodinToolStripMenuItem.Checked = false;
            balanceSignalToolStripMenuItem.Checked = false;
            doubleBalanceToolStripMenuItem.Checked = false;
            data.TC = 0;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void balanceSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleToolStripMenuItem.Checked = false;
            balanceGeterodinToolStripMenuItem.Checked = false;
            balanceSignalToolStripMenuItem.Checked = true;
            doubleBalanceToolStripMenuItem.Checked = false;
            data.TC = 2;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void balanceGeterodinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleToolStripMenuItem.Checked = false;
            balanceGeterodinToolStripMenuItem.Checked = true;
            balanceSignalToolStripMenuItem.Checked = false;
            doubleBalanceToolStripMenuItem.Checked = false;
            data.TC = 1;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void doubleBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleToolStripMenuItem.Checked = false;
            balanceGeterodinToolStripMenuItem.Checked = false;
            balanceSignalToolStripMenuItem.Checked = false;
            doubleBalanceToolStripMenuItem.Checked = true;
            data.TC = 3;

            data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
            DrawNomogramma();  //перерисовываем номограмму
            pictureBox1.Invalidate();  //обновление изображения
        }
        private void allCombinationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!allCombinationsToolStripMenuItem.Checked)
            {
                allCombinationsToolStripMenuItem.Checked = true;
                data.AllCombin = true;

                data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
                DrawNomogramma();  //перерисовываем номограмму
                pictureBox1.Invalidate();  //обновление изображения
            }
            else
            {
                allCombinationsToolStripMenuItem.Checked = false;
                data.AllCombin = false;

                data.CulcNomogramma(DrawLog);  //расчет номограммы комбинационных частот
                DrawNomogramma();  //перерисовываем номограмму
                pictureBox1.Invalidate();  //обновление изображения
            }
        }

        #region DrawFunctions

        //CONVERTOR_FUNCTIONS*********************************************************************        
        private Point MathToFiz(Point point, float[] pictureBoxMathBorder) 
        {
            float FXn = 0;
            float FXk = pictureBox1.Width;
            float FYn = pictureBox1.Height;
            float FYk = 0;

            float MXn = pictureBoxMathBorder[0];  // Xmin
            float MYn = pictureBoxMathBorder[1];  // Ymin
            float MXk = pictureBoxMathBorder[2];  // Xmax
            float MYk = pictureBoxMathBorder[3];  // Ymax

            int MXt = point.X;
            int MYt = point.Y;

            float FXt = FXn + (MXt - MXn) * (FXk - FXn) / (MXk - MXn);
            float FYt = FYn + (MYt - MYn) * (FYk - FYn) / (MYk - MYn);

            point.X = Convert.ToInt16(FXt);
            point.Y = Convert.ToInt16(FYt);

            return point;
        }  //преобразование координат из математических в физические, на входе ТОЧКА
        private Point MathToFiz(float MXt, float MYt, float[] pictureBoxMathBorder) 
        {
            Point point = new Point(0, 0);
            float FXn = 0;
            float FXk = pictureBox1.Width;
            float FYn = pictureBox1.Height;
            float FYk = 0;

            float MXn = pictureBoxMathBorder[0];  // Xmin
            float MYn = pictureBoxMathBorder[1];  // Ymin
            float MXk = pictureBoxMathBorder[2];  // Xmax
            float MYk = pictureBoxMathBorder[3];  // Ymax

            float FXt = FXn + (MXt - MXn) * (FXk - FXn) / (MXk - MXn);
            float FYt = FYn + (MYt - MYn) * (FYk - FYn) / (MYk - MYn);

            point.X = Convert.ToInt16(FXt);
            point.Y = Convert.ToInt16(FYt);

            return point;
        }  //преобразование координат из математических в физические, на входе желаемые координаты
        private float FizToMathX(Point point, float[] pictureBoxMathBorder) 
        {
            float FXn = 0;
            float FXk = pictureBox1.Width;

            float MXn = pictureBoxMathBorder[0];  //Xmin
            float MXk = pictureBoxMathBorder[2];  //Xmax

            float FXt = point.X;
            float MXt = MXn + (FXt - FXn)*(MXk - MXn)/(FXk - FXn);

            return MXt;
        }  //преобразование координат из физических в математические по X, на входе ТОЧКА
        private float FizToMathY(Point point, float[] pictureBoxMathBorder) 
        {
            float FYn = pictureBox1.Height;
            float FYk = 0;

            float MYn = pictureBoxMathBorder[1];  // Ymin
            float MYk = pictureBoxMathBorder[3];  // Ymax

            float FYt = point.Y;
            float MYt = MYn + (FYt - FYn) * (MYk - MYn) / (FYk - FYn);

            return MYt;
        }  //преобразование координат из физических в математические по Y, на входе ТОЧКА
        
        //DRAW_FUNCTIONS*********************************************************************
        private void DrawNomogramma()
        {            
            Cleans();  //очистка холста            
            DrawGrid();  //рисование сетки
            DrawMainLines();  //рисование двух линий основного преобразования
            DrawCombinLines();  //рисование комбинационных прямых
        }
        private void Cleans() 
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);                          
            }
            pictureBox1.Invalidate();
        }  //очистка холста
        private void DrawGrid()
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))            
                using (Pen LightGray = new Pen(Color.LightGray, 1))
                {
                    //рисование вертикальных линий, табулирование по Х
                    for (float i = originalPictureBoxMathBorder[0] + 0.1F; i <= originalPictureBoxMathBorder[2]; i += 0.1F)
                        g.DrawLine(LightGray, MathToFiz(i, originalPictureBoxMathBorder[1], pictureBoxMathBorder), MathToFiz(i, originalPictureBoxMathBorder[3], pictureBoxMathBorder));
                    //рисование горизонтальных линий, табулирование по Y
                    for (float i = originalPictureBoxMathBorder[1] + 0.1F; i <= originalPictureBoxMathBorder[3]; i += 0.1F)
                        g.DrawLine(LightGray, MathToFiz(originalPictureBoxMathBorder[0], i, pictureBoxMathBorder), MathToFiz(originalPictureBoxMathBorder[2], i, pictureBoxMathBorder));                                        
                }
            pictureBox1.Invalidate();
        }  //рисование сетки
        private void DrawMainLines()
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                using (Pen Black = new Pen(Color.Black, 1))
                {                    
                    //наклонная вниз
                    g.DrawLine(Black, MathToFiz(originalPictureBoxMathBorder[0], ((originalPictureBoxMathBorder[3] - originalPictureBoxMathBorder[1]) / 2 + originalPictureBoxMathBorder[1]), pictureBoxMathBorder), MathToFiz(originalPictureBoxMathBorder[2], originalPictureBoxMathBorder[3], pictureBoxMathBorder));
                    //наклонная вверх
                    g.DrawLine(Black, MathToFiz(originalPictureBoxMathBorder[0], ((originalPictureBoxMathBorder[3] - originalPictureBoxMathBorder[1]) / 2 + originalPictureBoxMathBorder[1]), pictureBoxMathBorder), MathToFiz(originalPictureBoxMathBorder[2], originalPictureBoxMathBorder[1], pictureBoxMathBorder));
                }
            }
            pictureBox1.Invalidate();
        }  //рисование осевой и наклоных линий
        private void DrawCombinLines()
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            using (Pen Gray = new Pen(Color.LightSlateGray, 1))                
                    for (int i = 1; i <= data.NLines; i++)                    
                        g.DrawLine(Gray, MathToFiz(data.NMP[i, 0], data.NMP[i, 1], pictureBoxMathBorder), MathToFiz(data.NMP[i, 2], data.NMP[i, 3], pictureBoxMathBorder));                    
        }  //программа построения комбинационных прямых        
        private void ReversibleFrameDraw(Color color, Point point1, Point point2)
        {
            ReversibleFrameSize.Width = PointToScreen(point2).X - PointToScreen(point1).X;
            ReversibleFrameSize.Height = PointToScreen(point2).Y - PointToScreen(point1).Y;
            point1.Y += toolStrip1.Height;
            ControlPaint.DrawReversibleFrame(new Rectangle(PointToScreen(point1), ReversibleFrameSize), color, FrameStyle.Dashed);
        }  //рисование прямоугольника выделения с учетом toolStrip1.Height

        #endregion

        //NATIVE_DATA_FUNCTIONS*********************************************************************
        private void DataUpload(int kp)
        {
            data = new Nomogramma();
            data.Kp = kp;
        }  //загрузка данных в форму Nomogramma из FAM
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void pictureBoxMathBorderResize()
        {
            //условия на случай ежели кто-то силно умный растанет прямоугольник выделения не сверху вниз и слева на право
            //установка новых границ при X1 < X2 и Y1 < Y2
            if (startPoint.X <= previousPoint.X && startPoint.Y <= previousPoint.Y)
            {
                float[] newBorder = { FizToMathX(startPoint, pictureBoxMathBorder), FizToMathY(startPoint, pictureBoxMathBorder), FizToMathX(previousPoint, pictureBoxMathBorder), FizToMathY(previousPoint, pictureBoxMathBorder) };
                pictureBoxMathBorder = newBorder;
            }

            //установка новых границ при X1 > X2 и Y1 > Y2
            if (startPoint.X >= previousPoint.X && startPoint.Y >= previousPoint.Y)
            {
                float[] newBorder = { FizToMathX(previousPoint, pictureBoxMathBorder), FizToMathY(previousPoint, pictureBoxMathBorder), FizToMathX(startPoint, pictureBoxMathBorder), FizToMathY(startPoint, pictureBoxMathBorder) };
                pictureBoxMathBorder = newBorder;
            }

            //установка новых границ при X1 < X2 и Y1 > Y2
            if (startPoint.X <= previousPoint.X && startPoint.Y >= previousPoint.Y)
            {
                float[] newBorder = { FizToMathX(startPoint, pictureBoxMathBorder), FizToMathY(startPoint, pictureBoxMathBorder), FizToMathX(previousPoint, pictureBoxMathBorder), FizToMathY(previousPoint, pictureBoxMathBorder) };
                pictureBoxMathBorder = newBorder;
            }

            //установка новых границ при X1 > X2 и Y1 < Y2
            if (startPoint.X >= previousPoint.X && startPoint.Y <= previousPoint.Y)
            {
                float[] newBorder = { FizToMathX(previousPoint, pictureBoxMathBorder), FizToMathY(startPoint, pictureBoxMathBorder), FizToMathX(startPoint, pictureBoxMathBorder), FizToMathY(previousPoint, pictureBoxMathBorder) };
                pictureBoxMathBorder = newBorder;
            }
        }  //установка новых математических границ       
    }
}
