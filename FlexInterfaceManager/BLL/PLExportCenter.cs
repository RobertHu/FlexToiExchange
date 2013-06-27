using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;
using System.IO;
using FlexInterface.Helper;
namespace FlexInterfaceManager.BLL
{
    public class PLExportCenter
    {
        private static PLExportCenter _Instance=null;
        private PLExportCenter() { }
        public static PLExportCenter Default
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PLExportCenter();
                }
                return _Instance;
            }
        }

        public void Process(PLData[] data, int startIndex,FileStream fs)
        {
            if (data == null) return;
            for(int i=0;i<data.Length;i++)
            {
                var source = data[i];
                int index = startIndex + i;
                ProcessDetail(source, index,fs);
            }
        }


        private void ProcessDetail(PLData source,int index,FileStream fs)
        {
            switch (source.Type)
            {
                case BusinessTypeEnum.RealizedPL:
                    var realizedPlData = source as RealizedPLData;
                    if (realizedPlData != null)
                    {
                        RealizedPlManager.Process(realizedPlData, index, fs);
                    }
                    break;
                case BusinessTypeEnum.FloatingPL:

                    var floatingPlData = source as FloatingPLData;
                    if (floatingPlData != null)
                    {
                        FloatingPlManager.Process(floatingPlData, index, fs);
                    }

                    break;
                case BusinessTypeEnum.InterestPL:
                case BusinessTypeEnum.StoragePL:
                case BusinessTypeEnum.Commission:
                case BusinessTypeEnum.Levy:
                    var otherPlData = source as InterestStorageLevyCommisionPLData;
                    if (otherPlData != null)
                    {
                        OtherPLManager.Process(otherPlData, index, fs);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
