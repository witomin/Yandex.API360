﻿using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Участник группы
    /// </summary>
    public class Member {
        /// <summary>
        /// Тип участника
        /// </summary>
        public MemberTypes? type { get; set; }
        /// <summary>
        /// Идентификатор участника группы
        /// </summary>
        public ulong id { get; set; }

        public override string ToString() {
            return $"{type}:{id}";
        }
    }

}
