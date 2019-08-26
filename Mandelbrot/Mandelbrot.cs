using System.Drawing;
using System.Windows.Forms;

namespace Mandelbrot
{
    public partial class Mandelbrot : Form
    {
        public Mandelbrot()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics draw = e.Graphics;

            float scale = 2F; // Storlek på Mandelbrot

            // Variabler för färg schemat som finns i koden
            int r = 0;
            int g = 0;

            
            bool green = false; // Bool variabel för färgerna

            // Kod för svart bakgrundsfärg för att få en bättre bild
            Brush backbrush = new SolidBrush(Color.Black);
            draw.FillRectangle(backbrush, 0, 0, 1378, 780); 

            // For loop, där lim är antal iterationer som ska utföras
            // desto större "lim < 1000" är, alltså t.ex. "lim < 5000"
            // desto noggranare blir bilden
            for (int lim = 1; lim < 1000; lim++)
            {

                // Skapar en "brush" som består av ett RGB färg
                // "FromArgb(g, r, r) där g-et och f-et ändrar ordningen på färgen
                Brush brush = new SolidBrush(Color.FromArgb(g, r, r));

                /* Forloop som körs antal gånger beroende av skalan
                Denna forloopen bestämmer de x-värden varje funktion ska vara placerad i
                genom att lägga funktionen i rätt x-axel, eftersom varje 
                x-värde kommer ha olika y-värden kommer en till forloop att finnas efter
                */
                for (double x = scale * -200; x < scale * 200; x++) // Forloop för x-axel
                {
                    for (double y = 0; y < scale * 200; y++) // Forloop för y-axeln av funktionen
                    {
                        double Rez = 0; // Första värdet är 0, kommer ändras efter varje iteration (kolla nästa forloop)
                        double Imz = 0; // Samma sak som Rez, kommer detta ändras efter varje iteration

                        double Cre = (x / ((scale * 100))); // Reella fragmentet av konstanten C (C = Cre + Cim)
                        double Cim = (y / ((scale * 100))); // Imaginära fragmentet av konstanten C

                        for (int k = 0; k < lim; k++)
                        {
                            double tempRez, tempImz;
                            // Temporära värden på Rez & Imz skapas eftersom efter
                            // varje iteration kommer dessa värden att förändras

                            tempRez = (Rez * Rez) - (Imz * Imz) + Cre; // Nya Rez + Cre
                            tempImz = (2 * (Rez * Imz)) + Cim; // Nya Imz + Cim

                            // Från funktion Zk = Zre + Zim kommer följande värde
                            // att sättas in och ge värde på funktionen
                            // DVS att t.ex. om z = 1 och f(z) är f(1) = c

                            Rez = tempRez; // Rez = Zre
                            Imz = tempImz; // Imz = Zim

                            /* Denna if statement följer formeln för |z| där det tas roten ur
                               men för att kunna spara på CPU användning och få datorn att
                               få programmet köras fortare, så tar vi bort sqrt och skriver > 4
                               om vi använder roten ur, så ska gränsen vara > 2
                               Meningen med denna statement är att kolla om värdet som vi
                               får för varje funktion är större än 4, för då skall den
                               ej ingå i bilden.
                            */

                            if ((Rez * Rez) + (Imz * Imz) > 4) // Hoppar ut när |z| går över 2
                            {
                                k = lim;
                            }
                        }

                        /* Här följs samma formel som föregående if-statement,
                           skillnaden är att här så ritar man alla funktioner som
                           ingår under intervallet 0 < |z| < 2
                         */
                        if ((Rez * Rez) + (Imz * Imz) <= 4)
                        {
                            draw.FillRectangle(brush, (float)x + (scale / 2 * 800), (float)y + (scale / 2 * 400), 1, 1);
                            draw.FillRectangle(brush, (float)x + (scale / 2 * 800), (scale / 2 * 400) - (float)y, 1, 1);
                        }
                    }
                }

                // Kod för färginställningar i mandelbrotten
                int limit = 10;

                if (r < 256 - limit || !green)
                {
                    r = r + limit;
                }

                if (r > 256 - limit || green)
                {
                    green = true;
                    if (g < 256 - limit)
                    {
                        g = g + limit;
                        r = r - limit;
                    }
                }
            }
        }

        private void Mandelbrot_Load(object sender, System.EventArgs e)
        {

        }
    }
}