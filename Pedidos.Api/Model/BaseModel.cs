﻿using System.Runtime.Serialization;

namespace Pedidos.API.Model
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember]
        public int Id { get; set; }
    }
}
