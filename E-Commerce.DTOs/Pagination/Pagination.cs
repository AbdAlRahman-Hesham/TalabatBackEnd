﻿namespace E_Commerce.DTOs.Pagination;

public class Pagination<T>
{
    public Pagination(int pageSize, int pageIndex,int count, ICollection<T> data)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        Data = data;
        Count = count;
    }

    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int Count { get; set; }
    public ICollection<T> Data { get; set; }
}
