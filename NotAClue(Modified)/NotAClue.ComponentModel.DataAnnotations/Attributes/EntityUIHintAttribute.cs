/// <copyright file="EntityUIHintAttribute.cs" company="NotAClue? Studios">
/// Copyright (c) 2011 All Right Reserved, http://csharpbits.notaclue.net/
///
/// This source is subject to the per project license unless otherwise agreed.
/// All other rights reserved.
///
/// </copyright>
/// <author>Stephen J Naughton</author>
/// <email>steve@notaclue.net</email>
/// <project>NotAClue.ComponentModel.DataAnnotations</project>
/// <date>24/07/2011</date>
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NotAClue.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityUIHintAttribute : Attribute
    {
        private IDictionary<string, object> _controlParameters;

        public IDictionary<string, object> ControlParameters
        {
            get
            {
                return this._controlParameters;
            }
        }

        /// <summary>
        /// Gets or sets the UI hint.
        /// </summary>
        /// <value>The UI hint.</value>
        public String UIHint { get; private set; }

        public EntityUIHintAttribute(string uiHint) : this(uiHint, new object[0])
        {
        }

        public EntityUIHintAttribute(string uiHint, params object[] controlParameters)
        {
            UIHint = uiHint;
            _controlParameters = BuildControlParametersDictionary(controlParameters);
        }

        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        private IDictionary<string, object> BuildControlParametersDictionary(object[] objArray)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            if ((objArray != null) && (objArray.Length != 0))
            {
                if ((objArray.Length % 2) != 0)
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Need even number of control parameters.", new object[0]));

                for (int i = 0; i < objArray.Length; i += 2)
                {
                    object obj2 = objArray[i];
                    object obj3 = objArray[i + 1];
                    if (obj2 == null)
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Control parameter key is null.", new object[] { i }));

                    string key = obj2 as string;
                    if (key == null)
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Control parameter key is not a string.", new object[] { i, objArray[i].ToString() }));

                    if (dictionary.ContainsKey(key))
                        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Control parameter key occurs more than once.", new object[] { i, key }));

                    dictionary[key] = obj3;
                }
            }
            return dictionary;
        }
    }
}