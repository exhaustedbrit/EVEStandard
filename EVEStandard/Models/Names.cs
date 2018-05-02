﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using EVEStandard.Enumerations;

namespace EVEStandard.Models
{
    public class Names : ModelBase<Names>
    {
        /// <summary>
        /// id integer
        /// </summary>
        /// <value>id integer</value>
        [Required]
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// name string
        /// </summary>
        /// <value>name string</value>
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    
        /// <summary>
        /// category string
        /// </summary>
        /// <value>category string</value>
        [Required]
        [JsonProperty("category")]
        public CategoryEnum? Category { get; set; }
    }
}