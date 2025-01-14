﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class CaveRoom : CaveRegion, IComparable<CaveRoom>
{
    public List<CaveCoord> edgeTiles = new List<CaveCoord>();   //靠边的坐标。
    public List<CaveRoom> connectedRooms = new List<CaveRoom>();//与其直接相连的房间。
    public bool isAccessibleFromMainRoom;               //是否能连接到主房间。
    public bool isMainRoom;                             //是否主房间（最大的房间）。

    public CaveRoom() { }

    public CaveRoom(List<CaveCoord> roomTiles) : base(roomTiles) { }

    public CaveRoom(List<CaveCoord> roomTiles, TileType[,] map) : base(roomTiles)
    {
        UpdateEdgeTiles(map);
    }

    // 更新房间边缘瓦片集
    public void UpdateEdgeTiles(TileType[,] map)
    {
        edgeTiles.Clear();

        // 遍历上下左右四格，判断是否有墙
        foreach (CaveCoord tile in tiles)
            for (int i = 0; i < 4; i++)
            {
                int x = tile.tileX + GameMathf.UpDownLeftRight[i, 0];
                int y = tile.tileY + GameMathf.UpDownLeftRight[i, 1];
                if (GameMathf.XYIsInRange(x, y, map.GetLength(0), map.GetLength(1)) && map[x, y] == TileType.Wall)
                {
                    edgeTiles.Add(tile);
                    continue;
                }
            }

    }

    //如果本身能连到主房间，标记其他相连的房间也能相连到主房间。
    public void MarkAccessibleFromMainRoom()
    {
        if (!isAccessibleFromMainRoom)
        {
            isAccessibleFromMainRoom = true;
            foreach (CaveRoom connectedRoom in connectedRooms)      //和他连一起的房间都能连到主房间。
                connectedRoom.MarkAccessibleFromMainRoom();
        }
    }

    // 连接房间
    public static void ConnectRooms(CaveRoom roomA, CaveRoom roomB)
    {
        //任何一个房间如果能连接到主房间，那另一个房间也能连到。
        if (roomA.isAccessibleFromMainRoom)
            roomB.MarkAccessibleFromMainRoom();
        else if (roomB.isAccessibleFromMainRoom)
            roomA.MarkAccessibleFromMainRoom();

        roomA.connectedRooms.Add(roomB);
        roomB.connectedRooms.Add(roomA);
    }

    // 是否连接另一个房间
    public bool IsConnected(CaveRoom otherRoom)
    {
        return connectedRooms.Contains(otherRoom);
    }

    // 比较房间大小
    public int CompareTo(CaveRoom otherRoom)
    {
        return otherRoom.RegionSize.CompareTo(RegionSize);
    }
}
