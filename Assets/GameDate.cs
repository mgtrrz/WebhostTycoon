using System.Collections;
using System.Collections.Generic;

public class GameDate {

	private static List<string> shortMonths = new List<string>() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
	private static List<string> longMonths = new List<string>() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

	public static string GetMonthNameFromInt(int i) {
		if (i < 1 || i > 12) {
			throw new System.ArgumentException("Int Parameter isn't within range 1 - 12", "i");
		}
		return shortMonths[i-1];
	}

	public static string GetLongMonthNameFromInt(int i) {
		if (i < 1 || i > 12) {
			throw new System.ArgumentException("Int Parameter isn't within range 1 - 12", "i");
		}
		return longMonths[i-1];
	}

	
}
