using ADS7828Api;
using BaseApi;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ADS7828TesterApp
{
    public partial class Form1 : Form
    {

        ADS7828[] m_ads7828 = new ADS7828[4];
        I2CBase m_i2c = new I2CMCP2221();
        bool m_connect = false;
        Thread m_thread;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_connect == false)
            {
                m_ads7828[0] = new ADS7828(m_i2c, 0, 0);
                m_ads7828[1] = new ADS7828(m_i2c, 0, 1);
                m_ads7828[2] = new ADS7828(m_i2c, 1, 0);
                m_ads7828[3] = new ADS7828(m_i2c, 1, 1);
                m_connect = true;
                m_thread = new Thread(ReadData);
                m_thread.Start();
            }
        }
        void ReadData()
        {
            TextBox[] txtAdc0 = { txtAdc0Ch0, txtAdc0Ch1, txtAdc0Ch2, txtAdc0Ch3, txtAdc0Ch4, txtAdc0Ch5, txtAdc0Ch6, txtAdc0Ch7 };
            TextBox[] txtAdc1 = { txtAdc1Ch0, txtAdc1Ch1, txtAdc1Ch2, txtAdc1Ch3, txtAdc1Ch4, txtAdc1Ch5, txtAdc1Ch6, txtAdc1Ch7 };
            TextBox[] txtAdc2 = { txtAdc2Ch0, txtAdc2Ch1 };
            TextBox[] txtAdc3 = { txtAdc3Ch0, txtAdc3Ch1, txtAdc3Ch2, txtAdc3Ch3, txtAdc3Ch4, txtAdc3Ch5, txtAdc3Ch6, txtAdc3Ch7 };

            while (m_connect)
            {
                for (int i = 0; i < 8; i++)
                {
                    m_ads7828[0].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                    float data = m_ads7828[0].ReadChannel();
                    txtAdc0[i].Text = data.ToString("0.0000");
                }

                for (int i = 0; i < 8; i++)
                {
                    m_ads7828[1].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                    float data = m_ads7828[1].ReadChannel();
                    txtAdc1[i].Text = data.ToString("0.0000");
                }

                for (int i = 0; i < 2; i++)
                {
                    m_ads7828[2].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                    float data = m_ads7828[2].ReadChannel();
                    txtAdc2[i].Text = data.ToString("0.0000");
                }

                for (int i = 0; i < 8; i++)
                {
                    m_ads7828[3].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                    float data = m_ads7828[3].ReadChannel();
                    txtAdc3[i].Text = data.ToString("0.0000");
                }

                Thread.Sleep(100);
            }
        }
    }
}
