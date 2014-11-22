﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class Neuron
    {
        public Link[] IncomingLinks;
        public double Power { get; set; }

        public Neuron(int LinksCount)
        {
            IncomingLinks = new Link[LinksCount];
            Power = 0;
        }
    }
}
