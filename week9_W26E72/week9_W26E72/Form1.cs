using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week9_W26E72.Entities;

namespace week9_W26E72
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = PersonCreate("");
            BirthProbabilities = BirthProbCreate("");
            DeathProbabilities = DeathProbCreate("");

            //sim
            Simulation();

        }

        private void Simulation()
        {
            for (int i = 2005; i <= 2024; i++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    //SimStep(i, Person);
                }
                int nbrMale = (from p in Population
                               where p.Gender == Gender.Male && p.IsAlive
                               select p).Count();
                int nbrFem = (from p in Population
                              where p.Gender == Gender.Female && p.IsAlive
                              select p
                                ).Count();
            }
        }

        private void SimStep(int year, Person p)
        {
            if (p.IsAlive == false) return;

            int age = year - p.BirthYear;

            var deathProb = (from d in DeathProbabilities
                            where d.Gender == p.Gender && d.Age == age
                            select d.P).FirstOrDefault();
            if (rng.NextDouble() <= deathProb)
            {
                p.IsAlive = false;
            }

            if (p.IsAlive && p.Gender.Equals(Gender.Female))
            {
                var birthProb = (from b in BirthProbabilities
                                 where b.Age == age
                                select b.P).FirstOrDefault();
                if (rng.NextDouble() <= birthProb)
                {
                    Person ps = new Person();
                    ps.BirthYear = year;
                    ps.ChildNum = 0;
                    p.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(p);
                }
            }
        }

        private List<DeathProbability> DeathProbCreate(string csvpath)
        {
            List<DeathProbability> death = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    DeathProbability dp = new DeathProbability();
                    dp.Gender = (Gender)Enum.Parse(typeof(Gender), line[0]);
                    dp.Age = int.Parse(line[1]);
                    dp.P = double.Parse(line[2]);
                    DeathProbabilities.Add(dp);
                }
            }
            return death;
        }

        private List<BirthProbability> BirthProbCreate(string csvpath)
        {
            List<BirthProbability> birth = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    BirthProbability bp = new BirthProbability();
                    bp.Age = int.Parse(line[0]);
                    bp.ChildNum = int.Parse(line[1]);
                    bp.P = int.Parse(line[2]);
                    BirthProbabilities.Add(bp);
                }
            }
            return birth;
        }

        private List<Person> PersonCreate(string csvpath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    Person p = new Person();
                    p.BirthYear = int.Parse(line[0]);
                    p.Gender = (Gender)Enum.Parse(typeof(Gender), line[1]);
                    p.ChildNum = int.Parse(line[2]);
                    Population.Add(p);
                }
            }
            return population;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string sFileName = ofd.FileName;        
            }
        }
    }
}
