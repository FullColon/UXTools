using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;

namespace UX_Affectiva_Research_Tool
{
    class Audio : RecordingToolBase
    {
        private WaveIn waveSource = null;
        private WaveFileWriter waveFile = null;
        private string filePath;

        public override bool startRecording()
        {
    


            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            string currentDirectory = Environment.CurrentDirectory;
            string newDir = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            filePath = newDir + @"\SaveFolder\audioFile.wav";

        
            waveFile = new WaveFileWriter(filePath, waveSource.WaveFormat);
            

            waveSource.StartRecording();

            return true;
        }
        public string getFilePath()
        {
            return filePath;
        }
        public override bool stopRecording()
        {
       


            waveSource.StopRecording();

            return true;
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }


        }
    }
}
