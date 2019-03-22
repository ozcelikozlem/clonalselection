using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace clonalselection
{
    public partial class Form1 : Form
    {
        double[,] baspop = new double[1000, 4];
        double[,] siralapop = new double[1000, 4];
        double[,] ntanepop = new double[1000, 4];
        double[,] siralaislem = new double[1000, 4];
        double[,] ntaneislem = new double[1000, 4];
        int[] klonlama1 = new int[10000];
        double[,] klonlama2 = new double[10000, 4];
        double[] x3 = new double[1000];
        double[] x2 = new double[1000];
        double[] x1 = new double[1000];
        double[] sx1 = new double[1000];
        double[] sx3 = new double[1000];
        double[] sx2 = new double[1000];
        double[] ex1 = new double[1000];
        double[] ex3 = new double[1000];
        double[] ex2 = new double[1000];
        double[] ex4 = new double[1000];
        int[] temp = new int[1000];
        double[,] mutasyonoran = new double[10000, 3];
        double[,] mutasyoncikar = new double[10000, 3];
        double[,] yenipopuret = new double[1000, 4];
        double[,] yenipopson = new double[1000, 4];
        double[,] cozumler = new double[1000, 4];
        double[,] eniyifit = new double[1000, 4];
        double gecici1;
        double gecici2;
        double gecici3;
        double sgecici1;
        double sgecici2;
        double sgecici3;
        double egecici1;
        double egecici2;
        double egecici3;
        double egecici4;


        public Form1()
        {
            InitializeComponent();
        }
        public void klonlama()
        {
            double npop = Convert.ToInt32(txtn.Text);
            double bas = Convert.ToInt32(txtbas.Text);
            double beta = Convert.ToDouble(txtbeta.Text);
            double klon;
            for (int k = 1; k <= npop; k++)
            {
                klon = Math.Round((beta * bas) / k);

                klonlama1[k - 1] = Convert.ToInt32(klon);
            }
            // label13.Text = klonlama1[0].ToString();

        }
        public void fitness()
        {
            int bas = Convert.ToInt32(txtbas.Text);
            for (int k = 0; k < bas; k++)
            {
                double fitness = 21.5 + baspop[k, 1] * Math.Sin(4 - Math.PI - baspop[k, 1]) + baspop[k, 2] - Math.Sin(20 - Math.PI - baspop[k, 2]);
                baspop[k, 3] = fitness;
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            btnyeni.Enabled = false;
            btncalistir.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnbas_Click(object sender, EventArgs e)
        {
            btnyeni.Enabled = true;
            btncalistir.Enabled = true;
            btnbas.Enabled = false;
            int bas = Convert.ToInt32(txtbas.Text);

            Random rnd = new Random();
            for (int i = 0; i < bas; i++)
            {
                //double rastgelex1 = -3 + rnd.NextDouble() * 12.1;
                baspop[i, 1] = -3 + rnd.NextDouble() * 12.1;
            }


            for (int j = 0; j < bas; j++)
            {
                double rastgelex2 = 4.1 + rnd.NextDouble() * 5.8;
                baspop[j, 2] = rastgelex2;
            }

            fitness();


            //iterasyon oluşturma..
            for (int x = 0; x < bas;x++)
            {
                baspop[x, 0] = x+1;
            }

            for (int b = 0; b < 4; b++)
            {

                for (int c = 0; c < bas; c++)
                {

                    dgridbaspop.Rows.Add();
                    // dgridsırala.Rows.Add();
                    dgridbaspop.Rows[c].Cells[b].Value = baspop[c, b].ToString();
                    // siralapop[c,b]= baspop[c, b];
                    // dgridsırala.Rows[c].Cells[b].Value = siralapop[c, b].ToString();
                }


            }

            //baspop fitness değerlerini tek diziye dönüştürme
            for (int u = 0; u < baspop.GetLength(0); u++)
            {
                x3[u] = baspop[u, 3];


            }

            for (int v = 0; v < baspop.GetLength(0); v++)
            {
                x2[v] = baspop[v, 2];


            }
            for (int y = 0; y < baspop.GetLength(0); y++)
            {
                x1[y] = baspop[y, 1];


            }




            //diziyi max a göre sıralama
            for (int t = x3.Length - 1; t >= 0; t--)
            {
                for (int w = 1; w <= x3.Length - 1; w++)
                {

                    if (x3[w - 1] < x3[w])
                    {
                        gecici3 = x3[w - 1];
                        x3[w - 1] = x3[w];
                        x3[w] = gecici3;

                        gecici2 = x2[w - 1];
                        x2[w - 1] = x2[w];
                        x2[w] = gecici2;

                        gecici1 = x1[w - 1];
                        x1[w - 1] = x1[w];
                        x1[w] = gecici1;

                    }
                }
            }

            //siralapop dizisinin içine sıralanmış diziyi atma
            for (int s = 0; s < baspop.GetLength(0); s++)
            {
                siralapop[s, 3] = x3[s];
                siralapop[s, 2] = x2[s];
                siralapop[s, 1] = x1[s];


            }
            //iterasyon oluşturma..
            for (int x = 0; x < bas; x++)
            {
                siralapop[x, 0] = x + 1;
            }


            //datagrid de siralapop u gösterme
            for (int r = 0; r < 4; r++)
            {

                for (int f = 0; f < bas; f++)
                {

                    dgridsırala.Rows.Add();
                    dgridsırala.Rows[f].Cells[r].Value = siralapop[f, r].ToString();
                }


            }
            //ilk n elemani seçme

            int npop = Convert.ToInt32(txtn.Text);

            for (int x = 0; x < npop; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    ntanepop[x, z] = siralapop[x, z];
                    dgrNPop.Rows.Add();
                    dgrNPop.Rows[x].Cells[z].Value = ntanepop[x, z].ToString();

                }

            }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btncalistir_Click(object sender, EventArgs e)
        {
            int iterasyon = Convert.ToInt32(txtit.Text);
            for (int cozum = 0; cozum < iterasyon; cozum++)
            {
                int npop = Convert.ToInt32(txtn.Text);

                //klonlama fonksiyonu ile işlemler

                int gecici4 = 0;
                klonlama();
                int toplamklon = 0;

                for (int z = 0; z < klonlama1.Length; z++)
                {

                    toplamklon += klonlama1[z];
                }



                //klonlanan sayıların diziye atılması
                for (int i = 0; i < npop; i++)
                {
                    for (int j = gecici4; j < toplamklon; j++)
                    {

                        for (int k = 1; k < 4; k++)
                        {

                            klonlama2[j, k] = ntanepop[i, k];

                        }


                    }
                    gecici4 = klonlama1[i] + gecici4;
                }

                //iterasyon oluşturma..
                for (int x = 0; x < gecici4; x++)
                {
                    klonlama2[x, 0] = x + 1;
                }


                //dizinin datagride atılması
                for (int r = 0; r < 4; r++)
                {

                    for (int f = 0; f < gecici4; f++)
                    {

                        dgridklon.Rows.Add();
                        dgridklon.Rows[f].Cells[r].Value = klonlama2[f, r].ToString();
                    }


                }
                Random rastoran1 = new Random();
                Random rastoran2 = new Random();


                //her hücre için mutasyon oranı atama
                for (int p = 0; p < gecici4; p++)
                {

                    double oranx1 = rastoran1.NextDouble();
                    double oranx2 = rastoran2.NextDouble();

                    mutasyonoran[p, 1] = oranx1;
                    mutasyonoran[p, 2] = oranx2;



                }
                // label13.Text += Convert.ToDouble(txtmut.Text) ;

                //x1 mutasyon 
                Random rastcikar = new Random();
                for (int s = 0; s < gecici4; s++)
                {

                    if (Convert.ToDouble(txtmut.Text) > mutasyonoran[s, 1])
                    {

                        double mutcikar = rastcikar.NextDouble();
                        klonlama2[s, 1] = klonlama2[s, 1] - mutcikar;


                    }

                }

                //x2 mutasyon
                Random rastcikar1 = new Random();
                for (int w = 0; w < gecici4; w++)
                {

                    if (Convert.ToDouble(txtmut.Text) > mutasyonoran[w, 2])
                    {

                        double mutcikar1 = rastcikar1.NextDouble();
                        klonlama2[w, 2] = klonlama2[w, 2] - mutcikar1;



                    }

                }


                //mutasyonlu dizi için yeniden düzenlenen fitnes değeri
                for (int k = 0; k < gecici4; k++)
                {
                    double fitness = 21.5 + klonlama2[k, 1] * Math.Sin(4 - Math.PI - klonlama2[k, 1]) + klonlama2[k, 2] - Math.Sin(20 - Math.PI - klonlama2[k, 2]);
                    klonlama2[k, 3] = fitness;
                }


                //mutasyonlu diziyi datagridde gösterme
                for (int b = 0; b < 4; b++)
                {

                    for (int a = 0; a < gecici4; a++)
                    {

                        dgMut.Rows.Add();
                        dgMut.Rows[a].Cells[b].Value = klonlama2[a, b].ToString();
                    }


                }

                //mutasyona ugramıs diziyi max a göre sıralama
                for (int u = 0; u < gecici4; u++)
                {
                    sx3[u] = klonlama2[u, 3];


                }

                for (int v = 0; v < gecici4; v++)
                {
                    sx2[v] = klonlama2[v, 2];


                }
                for (int y = 0; y < gecici4; y++)
                {
                    sx1[y] = klonlama2[y, 1];


                }

                for (int t = sx3.Length - 1; t >= 0; t--)
                {
                    for (int w = 1; w <= sx3.Length - 1; w++)
                    {

                        if (sx3[w - 1] < sx3[w])
                        {
                            sgecici3 = sx3[w - 1];
                            sx3[w - 1] = sx3[w];
                            sx3[w] = sgecici3;

                            sgecici2 = sx2[w - 1];
                            sx2[w - 1] = sx2[w];
                            sx2[w] = sgecici2;

                            sgecici1 = sx1[w - 1];
                            sx1[w - 1] = sx1[w];
                            sx1[w] = sgecici1;

                        }
                    }
                }

                //siralaislem dizisinin içine sıralanmış diziyi atma
                for (int s = 0; s < gecici4; s++)
                {
                    siralaislem[s, 3] = sx3[s];
                    siralaislem[s, 2] = sx2[s];
                    siralaislem[s, 1] = sx1[s];


                }

                //iterasyon oluşturma..
                for (int x = 0; x < npop; x++)
                {
                    siralaislem[x, 0] = x + 1;
                }
                //ilk n elemani seçme ve datagridin içine atma
                for (int x = 0; x < npop; x++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        ntaneislem[x, z] = siralaislem[x, z];
                        dgIslem.Rows.Add();
                        dgIslem.Rows[x].Cells[z].Value = ntaneislem[x, z].ToString();

                    }

                }
                //Yeni popülasyon üretme işlemleri
                int bas = Convert.ToInt32(txtbas.Text);

                Random rnd = new Random();
                //random yeni popülasyon üretme x1
                for (int i = 0; i < bas - npop; i++)
                {
                    yenipopuret[i, 1] = -3 + rnd.NextDouble() * 12.1;


                }

                Random rnd2 = new Random();
                //random yeni popülasyon üretme x2
                for (int j = 0; j < bas - npop; j++)
                {
                    double rastgelex2 = 4.1 + rnd2.NextDouble() * 5.8;
                    yenipopuret[j, 2] = rastgelex2;

                }
                //yenipopülasyon fitness üretme
                for (int k = 0; k < bas - npop; k++)
                {
                    double fitness = 21.5 + yenipopuret[k, 1] * Math.Sin(4 - Math.PI - yenipopuret[k, 1]) + yenipopuret[k, 2] - Math.Sin(20 - Math.PI - yenipopuret[k, 2]);
                    yenipopuret[k, 3] = fitness;
                }
                //ntaneislem dizisindeki elemanları yenipopülasyon dizisine atma
                for (int k = 0; k < npop; k++)

                {
                    for (int m = 1; m < 4; m++)

                    { yenipopson[k, m] = ntaneislem[k, m]; }
                }



                //yenipopülasyon dizisiyle üretilen diziyi toplama
                int d = 0;
                for (int x = npop; x < bas; x++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        yenipopson[x, z] = yenipopuret[d, z];


                    }
                    d++;
                }

                //iterasyon oluşturma..
                for (int x = 0; x < bas; x++)
                {
                    yenipopson[x, 0] = x + 1;
                }
                //oluşturulan yenipopülasyon dizisini datagride atma
                for (int c = 0; c < bas; c++)
                {
                    for (int v = 0; v < 4; v++)
                    {

                        dgYeniPop.Rows.Add();
                        dgYeniPop.Rows[c].Cells[v].Value = yenipopson[c, v].ToString();

                    }

                }

                //iterasyon oluşturma..
                for (int x = 0; x < iterasyon; x++)
                {
                    cozumler[x, 0] = x + 1;
                }

                for (int b = 1; b < 4; b++)
                {
                    cozumler[cozum, b] = ntaneislem[0, b];



                }

                for (int a = 0; a < 4; a++)
                {
                    dgEniyi.Rows.Add();
                    dgEniyi.Rows[cozum].Cells[a].Value = cozumler[cozum, a].ToString();
                }


            }

            //En iyi Çözüm
            for (int u = 0; u < iterasyon; u++)
            {
                ex3[u] = cozumler[u, 3];


            }

            for (int v = 0; v < iterasyon; v++)
            {
                ex2[v] = cozumler[v, 2];


            }
            for (int y = 0; y < iterasyon; y++)
            {
                ex1[y] = cozumler[y, 1];


            }

            for (int y = 0; y < iterasyon; y++)
            {
                ex4[y] = cozumler[y, 0];


            }

            for (int t = ex3.Length - 1; t >= 0; t--)
            {
                for (int w = 1; w <= ex3.Length - 1; w++)
                {

                    if (ex3[w - 1] < ex3[w])
                    {
                        egecici3 = ex3[w - 1];
                        ex3[w - 1] = ex3[w];
                        ex3[w] = egecici3;

                        egecici2 = ex2[w - 1];
                        ex2[w - 1] = ex2[w];
                        ex2[w] = egecici2;

                        egecici1 = ex1[w - 1];
                        ex1[w - 1] = ex1[w];
                        ex1[w] = egecici1;

                        egecici4 = ex4[w - 1];
                        ex4[w - 1] = ex4[w];
                        ex4[w] = egecici4;

                    }
                }
            }

            for (int s = 0; s < iterasyon; s++)
            {
                eniyifit[s, 3] = ex3[s];
                eniyifit[s, 2] = ex2[s];
                eniyifit[s, 1] = ex1[s];




                label13.Text = "Çözüm =" + eniyifit[0, 3];
                label15.Text = ex4[0] + ". iterasyon";
                label16.Text = "X1 =" + eniyifit[0, 1];
                label17.Text = "X2 =" + eniyifit[0, 2];
            }


            int bas1 = Convert.ToInt32(txtbas.Text);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0,bas1);
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, iterasyon);
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            for (int gr = 0; gr <= iterasyon; gr++)
            {

                chart1.Series[0].Points.AddXY(gr, cozumler[gr, 3]);
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }

            btncalistir.Enabled = false;


        }

        private void btnyeni_Click(object sender, EventArgs e)
        {
            //btnyeni.Enabled = false;
            //btncalistir.Enabled = false;
            //btnbas.Enabled = true;
            //this.dgrNPop.Rows.Clear();
            //this.dgEniyi.Rows.Clear();
            //this.dgIslem.Rows.Clear();
            //this.dgMut.Rows.Clear();
            //this.dgridbaspop.Rows.Clear();
            //this.dgridklon.Rows.Clear();
            //this.dgridsırala.Rows.Clear();
            //this.dgYeniPop.Rows.Clear();
            //label17.Text = "";
            //label13.Text = "";
            //label15.Text = "";
            //label16.Text = "";
            //txtbas.Text = "";
            //txtbeta.Text = "";
            //txtit.Text = "";
            //txtmut.Text = "";
            //txtn.Text = "";
            Application.Restart();

        }
    }
}
 

