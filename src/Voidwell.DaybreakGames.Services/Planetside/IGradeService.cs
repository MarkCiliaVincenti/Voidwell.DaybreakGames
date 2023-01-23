﻿using System.Collections.Generic;
using Voidwell.DaybreakGames.Domain.Models;

namespace Voidwell.DaybreakGames.Services.Planetside
{
    public interface IGradeService
    {
        IEnumerable<StatGrade> GetAllGrades();
        string GetGradeByDelta(double? delta);
    }
}
