using System.Collections;
using UnityEngine;

public static partial class Library {
	public static class NumberConverter {
		public static string ConvertedNumber(float number) {
			var stringToReturn                = "";

			if (number > 1000) {
				stringToReturn = (number / 1000) + "K";
				return stringToReturn;
			}
			else if (number > 1000000) {
				stringToReturn = (number / 1000000) + "M";
				return stringToReturn;
			}
		}
	}
}