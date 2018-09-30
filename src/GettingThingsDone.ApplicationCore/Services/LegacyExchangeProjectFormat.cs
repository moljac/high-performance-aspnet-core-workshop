﻿using System;
using GettingThingsDone.Contracts.Dto;
using GettingThingsDone.Contracts.Interface;
using static GettingThingsDone.Contracts.Interface.ServiceResult;

namespace GettingThingsDone.ApplicationCore.Services
{
    /// <summary>
    /// The legacy exchange project format is a fixed width format specified as follows:
    /// Title:        100 characters (<see cref="TitleWidth"/>)
    /// Description: 1000 characters (<see cref="DescriptionWidth"/>
    /// In addition, an exchange value can have an arbitrary number of leading and trailing whitespaces.
    /// </summary>
    internal class LegacyExchangeProjectFormat
    {
        private const int TitleWidth = 100;
        private const int DescriptionWidth = 1000;
        private const int TotalWidth = TitleWidth + DescriptionWidth;

        private const int TitleStart = 0;
 
        public static ServiceResult<ProjectDto> ToProjectDto(string legacyExchangeValue)
        {
            var value = legacyExchangeValue.AsSpan().Trim();
            if (value.Length != TotalWidth)
                return InvalidOperation<ProjectDto>("Invalid legacy exchange value.");

            var title = value.Slice(TitleStart, TitleWidth).Trim();

            return Ok(new ProjectDto
            {
                Name = new string(title)
            });
        }
    }
}