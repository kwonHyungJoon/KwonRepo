﻿using Microsoft.Win32;
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
using System.Windows.Forms.VisualStyles;

namespace 길찾기_퍼즐
{
    public partial class Form1 : Form
    {
        private const int GridSize = 5;
        private int catIndex = 21;
        private bool endValue = false;
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
<<<<<<< HEAD
                if (i == 5) // 도착지의 타일타입은 -1
                {
                    pictureBox.Tag = new PictureBoxInfo(i, -1);
                    continue;
                }
                pictureBox.Tag = new PictureBoxInfo(i, i == 21 ? 10 : 0);
=======
                int tileType = DetermineTileType(i);
                pictureBox.Tag = new PictureBoxInfo(i, tileType);
                SetPictureBoxImage(pictureBox, tileType);
>>>>>>> bbed2e124ac9fb488211b1f5d482d62b751f9814
            }

        }
        // 사진 넣는법: pictureBox.Image = Properties.Resources.MyImage;

        //타일 위치 설정
        private int DetermineTileType(int index)
        {
            switch(index)
            {
                case 21:
                    return 10; //우니 있는 세로 일자
                case 5:
                    return 5; // 도착
                case 15:
                    return 2; //네갈래
                case 18:
                    return 3; //세갈래
                case 3:
                    return 4; //곡선 길
                default:
                    return 0; //세로 일자
            }
        }

        //타일 세팅
        private void SetPictureBoxImage(PictureBox pictureBox, int tileType)
        {
            switch(tileType)
            {
                case 10:
                    pictureBox.Image = Properties.Resources.Tile_Token_0;
                    break;
                case 5:
                    pictureBox.Image = Properties.Resources.Tile_5;
                    break;
                case 2:
                    pictureBox.Image = Properties.Resources.Tile_2;
                    break;
                case 3:
                    pictureBox.Image = Properties.Resources.Tile_3;
                    break;
                case 4:
                    pictureBox.Image = Properties.Resources.Tile_4;
                    break;
                case 0:
                    pictureBox.Image = Properties.Resources.Tile_0;
                    break;
                default:
                    break;
            }
        }

        //클릭된 박스를 회전시키는 함수
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            PictureBoxInfo info = (PictureBoxInfo)clickedPictureBox.Tag;
            if(info.Index == 5)
            {
                lbTurnLog.Items.Add($"회전이 불가능한 타일입니다.");
                lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
                return;
            }
            if (clickedPictureBox != null)
            {
                switch (info.TileType)
                {
                    case 0: //세로로 일자 타일이면
                        info.TileType = 1;
                        clickedPictureBox.Image = Properties.Resources.nTile_1;
                        break;
                    case 1:  //가로로 일자 타일이면
                        info.TileType = 0;
                        clickedPictureBox.Image = Properties.Resources.nTile_0;
                        break;
                    case 10: //우니 있는 세로 일자 타일이면
                        info.TileType = 11;
<<<<<<< HEAD
                        clickedPictureBox.Image = Properties.Resources.nTile_11;
=======
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_1;
>>>>>>> bbed2e124ac9fb488211b1f5d482d62b751f9814
                        break;
                    case 11:  //우니 있는 가로 일자 타일이면
                        info.TileType = 10;
<<<<<<< HEAD
                        clickedPictureBox.Image = Properties.Resources.nTile_10;
=======
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_0;
                        break;
                    case 2:  //4방향 타일
                        info.TileType = 2;
                        clickedPictureBox.Image = Properties.Resources.Tile_2;
                        break;
                    case 22:  //우니 있는 4방향 타일
                        info.TileType = 22;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_2;
                        break;
                    case 3:  //3방향 타일
                        info.TileType = 3;
                        clickedPictureBox.Image = Properties.Resources.Tile_3;
                        break;
                    case 31:  //우니 있는 3방향 타일(좌상하)
                        info.TileType = 31;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_31;
                        break;
                    case 32:  //우니 있는 3방향 타일(좌우상)
                        info.TileType = 32;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_32;
                        break;
                    case 33:  //우니 있는 3방향 타일(좌우하)
                        info.TileType = 33;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_33;
                        break;
                    case 34:  //우니 있는 3방향 타일(상하우)
                        info.TileType = 34;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_34;
                        break;
                    case 4:  //2방향 타일
                        info.TileType = 4;
                        clickedPictureBox.Image = Properties.Resources.Tile_4;
                        break;
                    case 41:  //우니 있는 곡선 타일(상우)
                        info.TileType = 41;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_41;
                        break;
                    case 42:  //우니 있는 곡선 타일(좌하)
                        info.TileType = 42;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_42;
                        break;
                    case 43:  //우니 있는 곡선 타일(좌상)
                        info.TileType = 43;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_43;
                        break;
                    case 44:  //우니 있는 곡선 타일(하우)
                        info.TileType = 44;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_44;
                        break;
                    case 5:  //도착 타일
                        info.TileType = 5;
                        clickedPictureBox.Image = Properties.Resources.Tile_5;
                        break;
                    case 55:  //우니 있는 2방향 타일(하우)
                        info.TileType = 55;
                        clickedPictureBox.Image = Properties.Resources.Tile_Token_5;
>>>>>>> bbed2e124ac9fb488211b1f5d482d62b751f9814
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
            if (next.TileType < 0) // 도착지가 x타일
            {
                Address_Of_Box(catIndex).Image = Properties.Resources.nTile_0;
                playEnding();
                return true;
            }
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
            if (next.TileType < 0) // 도착지가 x타일
            {
                Address_Of_Box(catIndex).Image = Properties.Resources.nTile_1;
                playEnding();
                return true;
            }
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
                case 0: //세로 일자
                    info.TileType = 10;
<<<<<<< HEAD
                    Address_Of_Box(aim).Image = Properties.Resources.nTile_10;
=======
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_Token_0;
>>>>>>> bbed2e124ac9fb488211b1f5d482d62b751f9814
                    break;
                case 1: //가로 일자
                    info.TileType = 11;
<<<<<<< HEAD
                    Address_Of_Box(aim).Image = Properties.Resources.nTile_11;
=======
                    Address_Of_Box(aim).Image = Properties.Resources.Tile_Token_1;
>>>>>>> bbed2e124ac9fb488211b1f5d482d62b751f9814
                    break;
                case 10://세로 일자 + 우니
                    info.TileType = 0;
                    Address_Of_Box(aim).Image = Properties.Resources.nTile_0;
                    break;
                case 11: //가로 일자 + 우니
                    info.TileType = 1;
                    Address_Of_Box(aim).Image = Properties.Resources.nTile_1;
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
            if (endValue == true)
                return;
            TurnExistence(catIndex);
            TurnExistence(catIndex - GridSize);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 우니가 {catIndex / GridSize} 행, {catIndex % GridSize} 열로 이동합니다.");
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

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 우니가 {catIndex / GridSize + 1} 행, {catIndex % GridSize -1} 열로 이동합니다.");
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
            if (endValue == true)
                return;
            TurnExistence(catIndex);
            TurnExistence(catIndex + 1);

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 우니가 {catIndex / GridSize + 1} 행, {catIndex % GridSize + 1} 열로 이동합니다.");
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

            lbTurnLog.Items.Add($"{catIndex / GridSize + 1} 행, {catIndex % GridSize} 열에 있던 우니가 {catIndex / GridSize + 2} 행, {catIndex % GridSize} 열로 이동합니다.");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            catIndex = catIndex + GridSize;
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 저장된 파일은 길찾기퍼즐 파일 안에 저장됨.
            string textfile = @"../../MyTest.txt"; 

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
                MessageBox.Show("저장 완료");
            }
            else
            {
                MessageBox.Show("기존에 저장된 파일이 존재합니다.");
            }
        }

        private void playEnding()
        {
            lbTurnLog.Items.Add($"우니가 목적지에 도착했습니다!");
            lbTurnLog.SelectedIndex = lbTurnLog.Items.Count - 1;
            MessageBox.Show("우니가 목적지에 도착했습니다!");
            endValue = true;
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textfile = @"../../MyTest.txt";
            string line;
            int i = 1;
            if (File.Exists(textfile))
            {
                using (StreamReader sr = new StreamReader(textfile))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (i == 26)
                        {
                            catIndex = int.Parse(line);
                            break;
                        }

                        var pictureBox = (PictureBox)this.Controls.Find("pictureBox" + i, true).FirstOrDefault();
                        pictureBox.Tag = new PictureBoxInfo(i, int.Parse(line));
                        PictureBoxInfo info = (PictureBoxInfo)Address_Of_Box(i).Tag;

                        // 갱신된 타일타입에 맞춰 이미지 변경
                        switch (info.TileType)
                        {
                            case 0:
                                pictureBox.Image = Properties.Resources.nTile_0;
                                break;
                            case 1:
                                pictureBox.Image = Properties.Resources.nTile_1;
                                break;
                            case 10:
                                pictureBox.Image = Properties.Resources.nTile_10;
                                break;
                            case 11:
                                pictureBox.Image = Properties.Resources.nTile_11;
                                break;
                            default:
                                break;
                        }

                        i++;
                    }
                }
                MessageBox.Show("불러오기 완료");
            }
            else
            {
                MessageBox.Show("저장된 파일이 없습니다");
            }
        }
    }
}
