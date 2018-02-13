using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3Normalization
{
    class Mp3Info
    {
        private double mvarRadiodBGain;
        private double mvarAlbumdBGain;
        private double mvarModdB;
        private double mvarCurrMaxAmp;
        private int mvarCurrMaxGain;
        private int mvarCurrMinGain;

        private const double FIVELOG10TWO = 1.50514997831991;
        private const double NOREALNUM = -666.24601;

        public double MvarRadiodBGain { get; set; }
        public double MvarAlbumdBGain { get; set; }
        public double MvarModdB { get; set; }
        public double MvarCurrMaxAmp { get; set; }
        public int MvarCurrMaxGain { get; set; }
        public int MvarCurrMinGain { get; set; }


        public double AlbumdBGain()
        {
            if (mvarAlbumdBGain != NOREALNUM)
                return mvarAlbumdBGain + mvarModdB;
            else
                return mvarAlbumdBGain;
        }

        public int AlbumMp3Gain()
        {
            if (mvarAlbumdBGain != NOREALNUM)
                return Convert.ToInt16(Math.Round((mvarAlbumdBGain + mvarModdB) / FIVELOG10TWO));
            else
                return 0;
        }

        public double RadiodBGain()
        {
            if (mvarRadiodBGain != NOREALNUM)
                return mvarRadiodBGain + mvarModdB;
            else
                return mvarRadiodBGain;
        }

        public int RadioMp3Gain()
        {
            if (mvarRadiodBGain != NOREALNUM)
                return Convert.ToInt16(Math.Round((mvarRadiodBGain + mvarModdB) / FIVELOG10TWO));
            else
                return 0;
        }


        public int MaxNoclipMp3Gain()
        {
            double dblAdjust;

            if( (mvarCurrMaxAmp != NOREALNUM) && (mvarCurrMaxAmp < 1000000) && (mvarCurrMaxAmp > 0) )
            {
                dblAdjust = 4 * Math.Log(32767 / mvarCurrMaxAmp);
                if (Convert.ToDouble((int)dblAdjust) > dblAdjust)
                    return (int)dblAdjust - 1;
            }

            return 0;
        }

        public void AlterDb(double vData)
        {
            int intGainChange;

            if (mvarRadiodBGain != NOREALNUM)
                mvarRadiodBGain = mvarRadiodBGain + vData;
            if (mvarAlbumdBGain != NOREALNUM)
                mvarAlbumdBGain = mvarAlbumdBGain + vData;

            intGainChange = Convert.ToInt16(-vData / FIVELOG10TWO);
            if (mvarCurrMaxAmp != NOREALNUM)
                mvarCurrMaxAmp = mvarCurrMaxAmp * (2 ^ (intGainChange / 4));
            if (mvarCurrMaxGain != -1)
                mvarCurrMaxGain = mvarCurrMaxGain + intGainChange;
            if (mvarCurrMinGain != -1)
                mvarCurrMinGain = mvarCurrMinGain + intGainChange;
        }

        public void ResetVals()
        {
            mvarRadiodBGain = NOREALNUM;
            mvarAlbumdBGain = NOREALNUM;
            mvarCurrMaxAmp = NOREALNUM;
            mvarCurrMaxGain = -1;
            mvarCurrMinGain = -1;
        }

        public Mp3Info()
        {
            ResetVals();
            mvarModdB = 0;
        }
    }
}
