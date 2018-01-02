using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HouraiTeahouse.Options.UI {

    public class ResolutionDropdown : Dropdown {

        public ResolutionDropdown() {
            Options = Screen.resolutions.OrderBy(res => res.width)
                                    .ThenBy(res => res.height)
                                    .Select(res => string.Format("{0}x{1}", res.width, res.height))
                                    .Distinct().ToList();
        }

    }

}