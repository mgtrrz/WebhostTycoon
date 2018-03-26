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
		"Onson", "Mario", "Tony", "Jeromy", "Dwight", "Dwigt", "Bobson"
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
		"Marcus Gutierrez", "Ben Eaton", "Tomas Borosko"
	};

	private static string[] adjectives = {
		"able", "absolute", "accomplished", "acclaimed", "adventurous", "admirable", "afraid", "adored", "aggressive", "agile", "alarmed", "amazing", "angelic",
		"ancient", "anxious", "artistic", "athletic", "awkward", "awesome", "authentic", "attractive",
		"babyish", "barren", "basic", "beloved", "best", "bitter", "blank", "blissful", "blushing", "bogus", "bold", "bouncy", "brave", "brilliant", "bubbly",
		"bustling", "buttery", "buff",
		"calculating", "calm", "cautious", "careful", "caring", "cheap", "cheery", "cheerful", "clever", "cloudy", "colorful", "colorless", "comfortable", "common",
		"complex", "complicated", "considerate", "conventional", "corny", "courageous", "corrupt", "creamy", "creepy", "cultured", "critical", "cuddly", "curvy", 
		"cute", "cruel",
		"damaged", "dangerous", "dapper", "daring", "deadly", "decent", "delicious", "delayed", "defiant", "delightful", "delirious", "demanding", "dependable",
		"determined", "difficult", "discrete", "disastrous", "dismal", "distant", "disguised", "distant", "dizzy", "dramatic", "distributed", "dynamic",
		"eager", "earnest", "edible", "educated", "elaborate", "elastic", "elementary", "embarrased", "emotional", "empty", "enraged", "envious", "excellent",
		"exhausted", "excitable", "efficient", "enterprise", "extensible",
		"fabulous", "faint", "familiar", "famous", "fantastic", "fast", "favorable", "fearful", "feisty", "fickle", "filthy", "firm", "flaky", "flamboyant", "flashy",
		"flimsy", "flippant", "focused", "flustered", "forceful", "foolish", "frail", "fragrant", "fresh", "friendly", "frilly", "frozen", "fumbling", "fuzzy",
		"gargantuan", "gaseous", "gentle", "giant", "gifted", "giving", "glooming", "glorious", "gorgeous", "graceful", "greedy", "grim", "gross", "growing", "grubby",
		"gullible", "global", "granular",
		"hairy", "handsome", "handmade", "happy", "harmful", "harmless", "harsh", "healthy", "heavenly", "helpful", "hidden", "hilarious", "hollow", "honest", "honorable",
		"hopeful", "hot", "humble", "humiliating", "hungry", "husky",
		"icky", "ideal", "identical", "illegal", "idiotic", "illiterate", "imaginary", "immediate", "impeccable", "imperfect", "impolite", "important", "impractical",
		"impressive", "impure", "incompatible", "incredible", "infamous", "inferior", "innocent", "intelligent", "international", "irresponsible", 
		"jaded", "jagged", "jealous", "jolly", "joyful", "judicious", "jumbo", "juicy", "junior",
		"kindhearted", "klutzy", "knowledgeable", "knowing",
		"lame", "lanky", "lasting", "lavish", "lazy", "legitimate", "likable", "lively", "lonely", "loud", "low", "lengthy", "lucky", "loyal", "luxurious",
		"mad", "magnificent", "marvelous", "massive", "meager", "meaty", "meek", "mellow", "melodic", "menacing", "messy", "milky", "mindless", "miserable", "modern",
		"moist", "moral", "motionless", "muddy", "mundane", "mushy", "mysterious",
		"naive", "nautical", "naughty", "necessary", "neglected", "nervous", "nifty", "nocturnal", "noisy", "noxious", "nutritious",
		"obedient", "oily", "obvious", "odd", "offensive", "optimal", "organic", "ordinary", "original", "outstanding", 
		"palatable", "pale", "passionate", "peaceful", "perfect", "perky", "pessimistic", "plain", "pleasant", "plump", "polished", "polite", "pointless", "popular",
		"positive", "powerful", "practical", "pretty", "precious", "pricey", "pristine", "profitable", "pushy", "prudent", "puzzled",
		"quaint", "qualified", "queasy", "questionable", "quirky",
		"radiant", "ragged", "recent", "reasonable", "remarkable", "reliable", "responsible", "respectful", "repulsive", "revolving", "rewarding", "rigid", "roasted",
		"rotating", "rowdy", "royal", "rubbery", "rusty", "revolutionary", "seamless", "robust",
		"salty", "sarcastic", "satisfied", "scared", "scholarly", "scrawny", "selfish", "serious", "shallow", "shocking", "shameless", "sikly", "silly", "simplistic",
		"sinful", "sleepy", "slippery", "smug", "snarling", "sneaky", "soggy", "somber", "soulful", "sparkling", "spectacular", "spicy", "spirited", "spotless",
		"square", "standard", "sticky", "stimulating", "stormy", "strange", "strong", "stupdenous", "stupid", "submissive", "subtle", "suburban", "supportive", 
		"sweaty", "sweet", "sweltering", "sympathetic", "superior", 
		"tasty", "tedious", "tempting", "tender", "terrible", "terrific", "thankful", "thick", "thirsty", "tired", "tremendous", "tragic", "tricky", "trustworthy",
		"truthful", "transparent", 
		"ugly", "ultimate", "unacceptable", "uncommon", "uneven", "unfinished", "unfortunate", "unhappy", "unique", "unkempt", "unknown", "unlucky", "unrealistic",
		"unripe", "unruly", "unusual", "unwilling", "upset", "useless",
		"vacant", "valuable", "vengeful", "vibrant", "victorious", "vigorous", "violent", "virtual", "visible", "vivid", "verticle", "wireless",
		"warped", "wasteful", "wealthy", "weepy", "wet", "whispered", "whopping", "wicked", "wiggly", "winding", "wise", "wobbly", "wonderful", "worldly", "worried",
		"worthwhile", 
		"zaelous", "zesty"
	};

	private static string[] nouns = {
		"servers", "wreck", "applications", "architectures", "channels", "communities", "content", "deliverables", "metrics", "models", "network", "blockchain", "platforms",
		"portals", "synergies", "schemas", "solutions", "technologies", "computers", "switches", "routers", "ajax", "cloud", "space", "speed", "revolvers", "ideas", "systems",
		"data", "backups", "partnerships", "users", "art", "music", "food", "theory", "problem", "software", "internet", "cables", "media", "security", "keyboards", "laptops", 
		"floorboards", "profits", "clocks", "cooling", "customers", "managers", "books", "shield", "firewall", "containers", "machines", "humans", "robots", "cameras", "cells",
		"pdfs", "docs", "phone", "magazine", "category", "database", "inflation", "advice", "business", "game", "economy", "unicorns", "cookies", "javascript", "php", "wordpress",
		"cat", "dog", "horse", 
	};

	public static string[] tlds = {
		".com", ".org", ".net", ".edu", ".gov", ".co", ".io", ".me", ".us", ".biz", ".ca", ".ru", ".uk", ".es", ".xxx", ".site", ".website", ".shop", ".party", ".store",
		".ninja", ".lol", ".host", ".cloud", ".live", ".world", ".bid", ".global", ".blog", ".moe", ".art", ".cn", ".media", ".men", ".travel", ".review", ".family", 
		".studio", ".nyc", ".wtf", ".cat", ".guru", ".love", ".solutions", ".webcam", ".no", ".pe", ".af", ".fun", ".do", ".social", ".digital", ".chat", ".sexy", ".dog",
		".jp", ".sex", ".wedding", ".aaa", ".market", ".audio", ".photo", ".expert", ".gold", ".pizza", ".help", ".cash", ".game", ".sucks", ".software", ".business", 
		".games", ".cafe", ".systems", ".company", ".video", ".loan", ".movie", ".money", ".church", ".cool", ".technology", ".security", ".coffee", ".energy", ".beer", ".bi"
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

	public static string hostnameGenerator() {
		return adjectives[Random.Range(0, adjectives.Length)] + "-" + nouns[Random.Range(0, nouns.Length)];
	}

	public static string randomAdjective() {
		return adjectives[Random.Range(0, adjectives.Length)];
	}

	public static string randomNoun() {
		return nouns[Random.Range(0, nouns.Length)];
	}
}
