using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 길찾기_퍼즐
{
    //박스 정보를 보관한는 클래스 정의
    public class PictureBoxInfo
    {
        public int Index { get; set; }
        public int TileType { get; set; }

        public PictureBoxInfo(int index, int tileType)
        {
            Index = index;
            TileType = tileType;
        }
    }
}
