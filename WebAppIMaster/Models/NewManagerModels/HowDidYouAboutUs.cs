using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppIMaster.Models.NewManagerModels
{
    public class HowDidYouAboutUsCreate
    {
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public string SourceName_ru { get; set; }
        public string SourceName_kz { get; set; }
        public string Langcode { get; set; }
    }

    public class HowDidYouAboutUsEdit
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public string SourceName_ru { get; set; }
        public string SourceName_kz { get; set; }
        public string Langcode { get; set; }
    }
    public class HowDidYouAboutUsSelect
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public string SourceName { get; set; }
        public int? Clickcount { get; set; }
    }
    public class HowDidYouAboutUsSelectPopulation
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public string SourceName { get; set; }
        public int? Clickcount { get; set; }
    }

    public class HowDidYouAboutUsEditSelect
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public string SourceName_kz { get; set; }
        public string SourceName_ru { get; set; }
    }
}