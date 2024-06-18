using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 길찾기_퍼즐
{
    public partial class Form1 : Form
    {
        private const int GridSize = 5;
        private int catIndex = 21;
        //왼쪽 아래에서 시작
        public Form1()
        {
            InitializeComponent();
            InitializePuzzle();
        }

        private void InitializePuzzle()
        {
            for (int i = 1; i <= 25; i++)
            {
                var pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true).FirstOrDefault();
                pictureBox.Tag = new PictureBoxInfo(i, i == 21 ? 10 : 0);
            }

        }
        // 사진 넣는법: pictureBox.Image = Properties.Resources.MyImage;

        //클릭된 박스를 회전시키는 함수
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            PictureBoxInfo info = (PictureBoxInfo)clickedPictureBox.Tag;
            if (clickedPictureBox != null)
            {
                switch (info.TileType)
                {
                    case 0: //세로로 일자 타일이면
                        info.TileType = 1;
                        clickedPictureBox.Image = Properties.Resources.Tile_1;
                        break;
                    case 1:  //가로로 일자 타일이면
                        info.TileType = 0;
                        clickedPictureBox.Image = Properties.Resources.Tile_0;
                        break;
                    case 10: //캣 있는 세로 일자 타일이면
                        info.TileType = 11;
                        clickedPictureBox.Image = Properties.Resources.Tile_11;
                        break;
                    case 11:  //캣 있는 가로 일자 타일이면
                        info.TileType = 10;
                        clickedPictureBox.Image = Properties.Resources.Tile_10;
                        break;
                    default:
                        break;
                }
                clickedPictureBox.Invalidate();
                //다시 그리기

                // 회전 로그 추가
                lbTurnLog.Items.Add($"{info.Index / GridSize +1} 행, {info.Index % GridSize} 열에 해당하는 타일이 회전했습니다.");
                // 리스트박스의 화면이 맨 아래를 보이도록 조정
                lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;

            }
        }

        private void Unpassable_Message()
        {
            lbTurnLog.Items.Add($"해당 방향으로는 못 움직입니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
        }

        private bool IsPassable_Up()
        {
            if (catIndex <= GridSize) // 캣이 맨 윗 행에 있나
                return false;

            PictureBoxInfo now = (PictureBoxInfo)Address_Of_Box(catIndex).Tag;
            PictureBoxInfo next = (PictureBoxInfo)Address_Of_Box(catIndex - GridSize).Tag;

            if(now.TileType % 10 == 1) // 캣이 가로타일에 있나
                return false;
            if (next.TileType % 10 == 1) // 도착지가 가로타일인가
                return false;
            return true;
        }

        private bool IsPassable_Left()
        {
            if (catIndex % GridSize == 1) // 캣이 맨 왼쪽 열에 있나
                return false;

            PictureBoxInfo now = (PictureBoxInfo)Address_Of_Box(catIndex).Tag;
            PictureBoxInfo next = (PictureBoxInfo)Address_Of_Box(catIndex - 1).Tag;

            if (now.TileType % 10 == 0) // 캣이 세로타일에 있나
                return false;
            if (next.TileType % 10 == 0) // 도착지가 세로타일인가
                return false;
            return true;
        }

        private bool IsPassable_Right()
        {
            if (catIndex % GridSize == 0) // 캣이 맨 오른쪽 열에 있나
                return false;

            PictureBoxInfo now = (PictureBoxInfo)Address_Of_Box(catIndex).Tag;
            PictureBoxInfo next = (PictureBoxInfo)Address_Of_Box(catIndex + 1).Tag;

            if (now.TileType % 10 == 0) // 캣이 세로타일에 있나
                return false;
            if (next.TileType % 10 == 0) // 도착지가 세로타일인가
                return false;
            return true;
        }

        private bool IsPassable_Down()
        {
            if (catIndex > GridSize * (GridSize - 1)) // 캣이 맨 아래 행에 있나
                return false;

            PictureBoxInfo now = (PictureBoxInfo)Address_Of_Box(catIndex).Tag;
            PictureBoxInfo next = (PictureBoxInfo)Address_Of_Box(catIndex + GridSize).Tag;
            
            if (now.TileType % 10 == 1) // 캣이 가로타일에 있나
                return false;
            if (next.TileType % 10 == 1) // 도착지가 가로타일인가
                return false;
            return true;
        }

        private PictureBox Address_Of_Box(int aim)
        {
         
            string pictureBoxName = "pictureBox" + aim.ToString();
            PictureBox answer = this.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;
            return answer;
        }
        

        private void TurnExistence(int aim)
        {
            PictureBoxInfo info = (PictureBoxInfo)Address_Of_Box(aim).Tag;
            switch (info.TileType)
            {
                case 0:
                    info.TileType = 10;
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_10;
                    break;
                case 1:
                    info.TileType = 11;
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_11;
                    break;
                case 10:
                    info.TileType = 0;
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_0;
                    break;
                case 11:
                    info.TileType = 1;
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_1;
                    break;
                default:
                    break;
            }
        }

       

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (!IsPassable_Up())
            {
                Unpassable_Message();
                return;
            }
            TurnExistence(catIndex);
            TurnExistence(catIndex - GridSize);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 캣이 {catIndex / GridSize} 행, {catIndex % GridSize} 열로 이동합니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            catIndex = catIndex - GridSize;
            
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (!IsPassable_Left())
            {
                Unpassable_Message();
                return;
            }
            TurnExistence(catIndex);
            TurnExistence(catIndex - 1);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 캣이 {catIndex / GridSize + 1} 행, {catIndex % GridSize -1} 열로 이동합니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            catIndex = catIndex - 1;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (!IsPassable_Right())
            {
                Unpassable_Message();
                return;
            }
            TurnExistence(catIndex);
            TurnExistence(catIndex + 1);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 캣이 {catIndex / GridSize + 1} 행, {catIndex % GridSize + 1} 열로 이동합니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            catIndex = catIndex + 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (!IsPassable_Down())
            {
                Unpassable_Message();
                return;
            }
            TurnExistence(catIndex);
            TurnExistence(catIndex + GridSize);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 캣이 {catIndex / GridSize + 2} 행, {catIndex % GridSize} 열로 이동합니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            catIndex = catIndex + GridSize;
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textfile = @"c:\Users\kwonh\Desktop\MyTest.txt";

            // 파일이 존재하지 않으면
            if (!File.Exists(textfile))
            {
                using (StreamWriter sw = File.CreateText(textfile))
                {
                    for (int i = 1; i <= 25; i++)
                    {
                        var pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true).FirstOrDefault();
                        PictureBoxInfo info = (PictureBoxInfo)pictureBox.Tag;
                    sw.WriteLine(info.TileType);
                    }
                    sw.WriteLine(catIndex);
                }
            }
        }
    }
}
