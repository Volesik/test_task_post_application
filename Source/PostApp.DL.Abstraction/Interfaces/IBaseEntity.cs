﻿namespace PostApp.DL.Abstraction.Interfaces;

public interface IBaseEntity
{
    public long Id { get; set; }
    
    public DateTimeOffset CreatedWhen { get; set; }
    
    public DateTimeOffset UpdatedWhen { get; set; }
}