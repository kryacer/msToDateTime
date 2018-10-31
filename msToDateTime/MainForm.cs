using System;
using System.Windows.Forms;

namespace msToDateTime
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
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
                int currentYear = 1970;

                for (int i = 1970; i < 1970 + (staticDays / 364); i++)
                {
                    int minDaysInYear = 365;
                    if (IsLeapYear(i)) //Високосный 366
                        minDaysInYear = 366;
                    if (days >= minDaysInYear)
                    {
                        days -= minDaysInYear;
                        currentYear = i + 1;
                    }
                    else { break; }
                }

                int monthCounter = 1;
                bool leapYear = false;
                int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (IsLeapYear(currentYear))
                {
                    leapYear = true;
                    monthDays[1] = 29;
                }

                for (int i = 0; i < 12; i++)
                {
                    if (days - monthDays[i] >= 0)
                    {
                        days -= monthDays[i];
                        monthCounter++;
                    }
                    else { break; }
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
