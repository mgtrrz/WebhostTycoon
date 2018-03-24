using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator {

	private static string[] maleNames = {
		"Aaron", "Andy", "Alonso", "Arthur", "Arin", "Alan", "Arnold", "Adam", "Abraham", "Adan", "Andrew", "James", "John", "Johnny", "Robert", "William", "Drake",
		"David", "Brandon", "Branden", "Coleman", "Richard", "Charles", "Joseph", "Thomas", "Tom", "Chris", "Christopher", "Daniel", "Dan", "Danny",  "Don",
		"Donald", "Paul", "Mark", "Marcus", "George", "Jorge", "Ken", "Kenneth", "Steve", "Steven", "Stephen", "Ed", "Edward", "Ross", "Brett",
		"Edmond", "Brian", "Ronald", "Ron", "Anthony", "Kevin", "Jason", "Matt", "Matthew", "Mat", "Matais", "Michael", "Walker", "Emmanuel", "Nick", "Nicholas",
		"Gary", "Timothy", "Tim", "Jose", "Larry", "Jeff", "Jeffrey", "Geoff", "Geoffrey", "Frank", "Scott", "Eric", "Jared", "Jake", "Alex", "Omar",
		"Ray", "Raymond", "Greg", "Gregory", "Josh", "Joshua", "Jerry", "Dennis", "Walter", "Patrick", "Peter", "Doug", "Douglas", "Austin", "Ian",
		"Henry", "Carl", "Ryan", "Roger", "Joe", "Juan", "Justin", "Jonathan", "Terry", "Keith", "Sam", "Samuel", "Will", "Ben", "Bob", "Jon", "Oliver",
		"Bobby", "Shawn", "Clarence", "Craig", "Todd", "Phillip", "Phil", "Randy", "Howard", "Eugene", "Javier", "Jordan", "Guy", "Cody", "Ted",
		"Darren", "Tyrone", "Darryl", "Cory", "Adrian", "Jaime", "Wade", "Cecil", "Luke", "Nolan", "Dustin", "Nate", "Nathan", "Zach", "Zachary", "Sleve",
		"Onson", "Mario", "Tony", "Jeromy", "Dwight", "Dwigt", "Bobson",
	};

	private static string[] femaleNames = {
		"Mary", "Patricia", "Linda", "Glenda", "Barbara", "Elizabeth", "Eliza", "Beth", "Jenn", "Jennifer", "Maria", "Susan", "Susie", "Dorothy", "Lisa",
		"Nancy", "Karen", "Kaitlyn", "Kaity", "Kait", "Betty", "Helen", "Ellen", "Sandra", "Donna", "Carol", "Ruth", "Sharon", "Michelle", "Laura", "Sarah",
		"Kim", "Kimberly", "Deborah", "Jessica", "Jess", "Shirley", "Angela", "Melissa", "Brenda", "Amy", "Anna", "Rebecca", "Pam", "Pamela", "Martha", "Amanda",
		"Steph", "Stephanie", "Carolyn", "Christine", "Alex", "Marie", "Courtney", "Ann", "Frances", "Alice", "Diane", "Julie", "Heather", "Evelyn", "Joan", "Ashley",
		"Ash", "Rose", "Aurora", "Nicole", "Christina", "Kathy", "Jane", "Rachel", "Andrea", "Anne", "Sara", "Julia", "Ruby", "Tina", "Paula", "Diana", "Emily", "Robin",
		"Tracy", "Brittany", "Janna", "Tiff", "Tiffany", "Victoria", "Slyvia", "Dawn", "Annie", "Amber", "Eva", "April", "Leslie", "Carrie", "Pauline", "Shannon",
		"Megan", "Danielle", "Eleanor", "Alicia", "Allison", "Erin", "Veronica", "Michele", "Michelle", "Lauren", "Sally", "Beatrice", "Audrey", "Holly",
		"Laurie", "Beth", "Vicki", "Lucy", "Jessie", "Natalie", "Stacey", "Joy", "Georgia", "Claudia", "Liz", "Marci", "Araceli", "Reva", "Mari", "Kasey", "Casey", 
		"Eve", "Sofia", "Enya", "Addis", "Sabrina", "Brianna", "Bri", "Dominique", "Renata", "Melody", "Shayla"
	};

	private static string[] lastNames = {
		"Smith", "Smoth", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "White", "Harris", "Martin",
		"Jackson", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Hall", "Allen", "Young", "Hernandez", "King",
		"Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Perez", "Phillips", "Campbell", "Evans", "Parker", "Sanchez",
		"Cook", "Morgan", "Bell", "Richardson", "Gutierrez", "Cox", "Brooks", "Price", "Bennett", "Wood", "Perry", "Patterson", "Long", "Sanders", "Howard",
		"Flores", "Simmons", "Diaz", "Wallace", "Cole", "Reynolds", "Fisher", "Ellis", "Harrison", "Cruz", "Gomez", "Murray", "Freeman", "Wells", "Webb",
		"Simpson", "Porter", "Hunter", "McDonald", "Holmes", "Hunt", "Spencer", "Christoff", "Odneal", "Howle", "Cardenas", "Newman", "Curtis", "Moreno", "Cates",
		"Garmon", "Reinhardt", "Arnold", "Steiner", "Farmer", "Eaton", "Turton", "Cousins", "Borosko", "Kiowski", "Garduna", "Alcorn", "Powell", "Jennings",
		"Leal", "Anthony", "Thompson", "Cortinas", "Evans", "Guzman", "Son", "Dustice", "McDichael", "Sweemy", "Archideld", "Smorin", "McSriff", "Mixon", "Chamgerlain",
		"Nogilny", "Smehrik", "Dugnutt", "Truk", "Wesrey", "Bonzalez", "Dandleton"
	};

	private static string[] specialNames = {

	};

	public static string generateRandomName(string gender) {
		string firstName = "";
		if ( gender.ToLower() == "male" ) {
			firstName = maleNames[Random.Range(0, maleNames.Length)];
		} else if ( gender.ToLower() == "female" ) {
			firstName = femaleNames[Random.Range(0, femaleNames.Length)];
		}

		string lastName = lastNames[Random.Range(0, lastNames.Length)]; 
		
		return firstName + " " + lastName;
	}
}
