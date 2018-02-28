using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msToDateTime
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //long msec = Convert.ToInt64(InputBox.Text);
                long msec = (long)(dateTimePicker1.Value - new DateTime(1970, 1, 1)).TotalMilliseconds;
                long sec = msec / 1000;
                    long msecRest = msec % 1000;
                long min = sec / 60;
                    long secRest = sec % 60;
                long hour = min / 60;
                    long minRest = min % 60;
                long days = (hour / 24);
                    long hourRest = hour % 24;

                long staticDays = days;

                int yearCounter = 0;
                
                for (int i = 1970; i < 1970 + (staticDays / 364); i++)
                {
                    if ((i % 4 == 0 && i % 100 != 0) || i % 400 == 0) //Високосный 366
                    {
                        if (days > 365)
                        {
                            days -= 366;
                            yearCounter++;
                        }
                        else { break; }
                    }
                    if ((i % 100 == 0 && i % 400 != 0) || i % 4 != 0) //Не високосный 365
                    {
                        if (days > 364)
                        {
                            days -= 365;
                            yearCounter++;
                        }
                        else { break; }
                    }
                }
                int currentYear = 1970 + yearCounter;
                int monthCounter = 1;
                bool leapYear = false;
                if ((currentYear % 4 == 0 && currentYear % 100 != 0) || currentYear % 400 == 0)
                {
                    leapYear = true;
                }
                if ((currentYear % 100 == 0 && currentYear % 400 != 0) || currentYear % 4 != 0)
                {
                    leapYear = false;
                }
                int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                for (int i = 0; i < 12; i++)
                {
                    if (leapYear == true)
                    {
                        if (days - monthDays[i] > 0)
                        {
                            if (i == 1)
                            {
                                days -= 29;
                                monthCounter++;
                            }
                            else
                            {
                                days -= monthDays[i];
                                monthCounter++;
                            }

                        }
                        else { break; }
                    }
                    else
                    {
                        if (days - monthDays[i] > 0)
                        {
                            days -= monthDays[i];
                            monthCounter++;
                        }
                        else { break; }
                    }
                }
                long currentDay = 1 + days;

                DateBox.Text = leapYear.ToString() + " " + currentDay.ToString() + "." + monthCounter.ToString() + "." + currentYear.ToString();

                TimeBox.Text = hourRest.ToString() + ":" + minRest.ToString() + ":" + secRest.ToString() + ":" + msecRest.ToString();

                
                CorrectOutputBox.Text = new DateTime(1970, 1, 1).AddMilliseconds(msec).ToString("dd.MM.yyyy HH:mm:ss:fff");
            }
            catch (FormatException ex) { MessageBox.Show(ex.Message); }
        }
    }
}
