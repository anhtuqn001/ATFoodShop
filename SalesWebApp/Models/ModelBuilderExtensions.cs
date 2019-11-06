using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().HasData(
                new Food
                {
                    FoodId = -1,
                    Name = "Spaghetti",
                    Price = 20,
                    PhotoPath = "2cce4572-4583-4b6c-847f-f32fb6c34b46_spaghetti.jpg",
                    LongDescription = "<p>Spaghetti is a long, thin, solid, cylindrical pasta. Spaghettoni is a thicker form of spaghetti, while capellini is a very thin spaghetti. It is a staple food of traditional Italian cuisine. Like other pasta, spaghetti is made of milled wheat and water and sometimes enriched with vitamins and minerals. Authentic Italian spaghetti is made from durum wheat semolina, but elsewhere it may be made with other kinds of flour. Usually the pasta is white because refined flour is used, but whole wheat flour may be added.</p><p>Originally, spaghetti was notably long, but shorter lengths gained in popularity during the latter half of the 20th century and now it is most commonly available in 25–30 cm(10–12 in) lengths.A variety of pasta dishes are based on it, and it is frequently served with tomato sauce or meat or vegetables.</p> ",
                    ShortDescription = "Spaghetti is a long, thin, solid, cylindrical pasta."
                },
                new Food
                {
                    FoodId = -2,
                    Name = "Beef Steak With Rice",
                    Price = 30,
                    PhotoPath = "f96c4c13-d83a-47ce-907b-ecb089afe4e3_beefsteak.jpg",
                    LongDescription = "<p>Combine yogurt, garam masala, garlic powder, paprika and salt in small bowl. Spread 1/3 cup yogurt mixture over steak. Reserve remaining yogurt mixture for sauce. Place beef steak in food-safe plastic bag; turn steak to coat. Close bag securely and marinate in refrigerator 6 hours or as long as overnight.</p><p> Remove steak from marinade; discard marinade. Place steak on grid over medium, ash-covered coals. Grill, covered, 11 to 16 minutes(over medium heat on preheated gas grill, covered, 16 to 21 minutes) for medium rare (145°F) to medium(160°F) doneness, turning occasionally.Meanwhile, grill onion slices, covered, 11 to 15 minutes. Remove steak from grill; let stand 3 to 5 minutes.</p><p> Meanwhile, heat remaining sauce and water in small saucepan over medium heat 2 to 3 minutes.Cut steak lengthwise in half, then across the grain into thin slices. Cut onions into bite-sized pieces. Combine rice and peas in large bowl. Divide rice mixture evenly among plates. Serve with beef, onions and sauce.</p> ",
                    ShortDescription = "Filipino beef steak is a saucy beef dish with lots of rice."
                },
                new Food
                {
                    FoodId = -3,
                    Name = "Pizza",
                    Price = 50,
                    PhotoPath = "fa29f8ad-0b17-4a07-9546-a01222899995_pizza.jpg",
                    LongDescription = "<p>Pizza is a savory dish of Italian origin, consisting of a usually round, flattened base of leavened wheat-based dough topped with tomatoes, cheese, and various other ingredients (anchovies, olives, meat, etc.) baked at a high temperature, traditionally in a wood-fired oven. A small pizza is sometimes called a pizzetta.</p><p> In Italy, pizza served in formal settings, such as at a restaurant, is presented unsliced and eaten with the use of a knife and fork. In casual settings it is cut into wedges to be eaten while held in the hand.</p><p> The term pizza was first recorded in the 10th century in a Latin manuscript from the Southern Italian town of Gaeta in Lazio, on the border with Campania.Modern pizza was invented in Naples, and the dish and its variants have since become popular in many countries. It has become one of the most popular foods in the world and a common fast food item in Europe and North America, available at pizzerias(restaurants specializing in pizza), restaurants offering Mediterranean cuisine, and via pizza delivery.Many companies sell ready-baked frozen pizzas to be reheated in an ordinary home oven.</p><p> The Associazione Verace Pizza Napoletana(lit.True Neapolitan Pizza Association) is a non - profit organization founded in 1984 with headquarters in Naples that aims to promote traditional Neapolitan pizza. In 2009, upon Italy's request, Neapolitan pizza was registered with the European Union as a Traditional Speciality Guaranteed dish, and in 2017 the art of its making was included on UNESCO's list of intangible cultural heritage.</p>",
                    ShortDescription = "Pizza is dish consisting of bread, olive oil, oregano, tomato, olives."
                },
                new Food
                {
                    FoodId = -4,
                    Name = "Fried Chicken",
                    Price = 30,
                    PhotoPath = "5044c09f-7f26-4849-b298-2f758f8593c1_fried-chicken.jpg",
                    LongDescription = "<p>Southern fried chicken, also known simply as fried chicken, is a dish consisting of chicken pieces which have been coated in a seasoned batter and pan-fried, deep fried, or pressure fried. The breading adds a crisp coating or crust to the exterior of the chicken while retaining juices in the meat. Broiler chickens are most commonly used.</p><p> The first dish known to have been deep fried was fritters, which were popular in the European Middle Ages.However, it was the Scottish who were the first Europeans to deep fry their chicken in fat(though without seasoning). Meanwhile, a number of West African peoples had traditions of seasoned fried chicken(though battering and cooking the chicken in palm oil). Scottish frying techniques and West African seasoning techniques were combined by enslaved Africans and African - Americans in the American South.</p> ",
                    ShortDescription = "Pizza is dish consisting of bread, olive oil, oregano, tomato, olives."
                },
                new Food
                {
                    FoodId = -5,
                    Name = "Roasted Chicken",
                    Price = 40,
                    PhotoPath = "f1a856be-1648-4033-a8ce-0b9ad967bcdf_RoastedChicken.jpg",
                    LongDescription = "<p>The best roast chicken has garlic herb butter under and on the skin, is stuffed with lemon and herbs, roasted until it’s crispy and deep golden on the outside, and juicy on the inside.</p><p> It’s really easy to prepare a whole chicken for roasting. The trick is to use an upside down dessert spoon to loosen the skin – the shape hugs the curves of the chicken and it doesn’t tear the skin.</p><p> Then simply spoon the garlic - herb - butter under the skin then spread it from the outside or hold it upright and drizzle in (see photos below) – whatever you find easier!</p> ",
                    ShortDescription = "Traditional roasted chicken with thyme and onion stuffing."
                },
                new Food
                {
                    FoodId = -6,
                    Name = "Vietnamese Bread",
                    Price = 10,
                    PhotoPath = "4ca2ec4c-21b8-4ea8-9402-57f7b37029f0_bread.jpg",
                    LongDescription = "<p>Bánh mì or banh mi is the Vietnamese word for bread. In Vietnamese cuisine, it also refers to a type of baguette which is often split lengthwise and filled with various savory ingredients as a sandwich and served as a meal. Plain banh mi is also eaten as a staple food.</p><p> A typical Vietnamese sandwich is a fusion of meats and vegetables from native Vietnamese cuisine such as chả lụa(pork sausage), coriander leaf(cilantro), cucumber, pickled carrots, and pickled daikon combined with condiments from French cuisine such as pâté,along with chili and mayonnaise.[6] However, a wide variety of popular fillings are used, from xíu mại to ice cream.In Vietnam, sandwiches are typically eaten for breakfast or as a snack; they are considered too dry for lunch or dinner.</p><p> The baguette was introduced to Vietnam in the mid–19th century, when Vietnam was part of French Indochina, and became a staple food by the early 20th century.During the 1950s, a distinctly Vietnamese style of sandwich developed in Saigon, becoming a popular street food.Following the Vietnam War, Overseas Vietnamese popularized the bánh mì sandwich in countries such as Australia, Canada and the United States.</p> ",
                    ShortDescription = "Bread is split lengthwise and filled with various savory ingredients."
                },
                new Food
                {
                    FoodId = -7,
                    Name = "Crab With Chilli Sauce",
                    Price = 20,
                    PhotoPath = "d83dda1a-f0aa-45b1-ac08-862b5230a323_crab.jpg",
                    LongDescription = "<p>Crabs need to be rinsed well, cleaned, and cut into pieces. Check out this video by Sydney Fish Market on how to do it, or get your fishmonger to do it for you. I keep the creamy part inside the top shell for the extra crab flavor that it lends to the dish. If using a wok with lid, make sure that it's supported on the stove well. I used ABC brand hot chili sauce. I found that I didn't need to adjust my flavorings, but you may want to adjust so that it has a balance of sweet, salty, and heat from the chilies. Take care when eating the sauce—it may have bits of shell!</p><h3> Directions </h3><p>1. In small bowl, stir cornstarch with 2 tablespoons water; set aside. In large wok with lid(or Dutch oven), heat oil over medium heat until shimmering. Stir in shallots, ginger, garlic, and chilies. Cook until fragrant, stirring, about 1 minute.</p><p>2. Add crab pieces and broth. Increase heat to medium high and bring to a boil. Cover loosely and gently boil(decrease heat if necessary), until crab has turned red and is nearly cooked through, about 6 minutes.</p><p>3. Remove cover and stir in tomato paste and chili sauce. Simmer 1 minute and season to taste with salt, sugar, or chili sauce.Stir in cornstarch and bring to boil to thicken.</p><p>4. Remove from heat and stir in egg.Stir in green onions. Ladle into serving dish, sprinkle with Chinese parsley, and serve.</p> ",
                    ShortDescription = "Singapore chili crab made with a rich-tasting red tomato-chili sauce"
                },
                new Food
                {
                    FoodId = -8,
                    Name = "Grilled Octopus",
                    Price = 30,
                    PhotoPath = "0fb42bb7-047c-4d28-a254-316c5f756e77_Octopus.jpg",
                    LongDescription = "<p>For most Americans, the idea of cooking octopus sounds challenging, unsettling and plain old scary. Fear not, cooking these cephalopods is not hard and makes for a fun group activity. This octopus recipe is almost foolproof if you follow my instructions. I provide two different ways to tenderize it; both work beautifully.</p><p> Look for whole, frozen octopus.A good fishmonger can find one for you.I purchase mine at Santa Monica Seafoods in Costa Mesa, California. A whole Spanish octopus will typically weigh between 4 and 7 pounds. They are cleaned before frozen, but will include the head and the beak. (More on that later.) A 4 - 7 pound octopus sounds like enough to feed an army, but they shrink a lot.Once prepared it will provide an appetizer quantity for eight or dinner for four.These octopus are not inexpensive.Plan on spending $55 to $70 for one 4 - 7 pound octopus.</p><p> The good news is that most octopus you buy in the US will be frozen and cleaned.Thaw the octopus overnight in the fridge.Now comes the part where you need to get over your squeamishness. You need to remove the beak.Yep, the beak.It is the only hard part of this gelatinous creature. The beak is located inside the octopus mouth and is used to cut into its prey.</p>",
                    ShortDescription = "Octopus is grilled with carrots, celery, onion, garlic, olive oil."
                },
                new Food
                {
                    FoodId = -9,
                    Name = "Steamed Fish",
                    Price = 50,
                    PhotoPath = "4c7ec914-a02c-42d3-b281-a78153f9208a_steamfish.jpg",
                    LongDescription = "<p>In many Chinese families, steamed fish are always listed top of their most cooked recipes. Steamed fish with ginger and spring onions is one of the easiest and best steamed fish recipe available. There are many reasons why steaming the fish is preferred over other methods of cooking. Firstly, it is much healthier and secondly, most of the nutrients of the fish are retained. Ensure that the fish is fresh, of course!</p><p> The main flavour to the gravy is the soy sauce and the hint of sweetness which comes along with it.Using Chinese light soy sauce would be the best.Additionally, the gravy is sweetened with rock sugar as it is very subtle in sweetness.Although you may substitute it with regular sugar, the sweetness of the rock sugar goes well with this steamed fish gravy.</p><p> It is considered auspicious to have fish served during the reunion dinner, which takes place a day before Chinese New Year as the pronunciation of fish in Chinese(“Yu”) sounds similar to the word surplus.Hence, having a whole fish to begin the year signifies the start of a new year with surplus and abundance.</p><p> Despite being a fairly easy dish to prepare, the flesh of the fish will be tough if it is steamed for too long.Depending on the size and thickness of the fish, steaming a fish normally takes around 15 to 20 minutes.</p> ",
                    ShortDescription = "Steamed fish has always been a great dish to pair with rice."
                },
                new Food
                {
                    FoodId = -10,
                    Name = "Steamed King Crab",
                    Price = 60,
                    PhotoPath = "c10a1f29-58c3-4bd4-b874-1b804a4b3304_kingcrab.png",
                    LongDescription = "<p>When our Alaska King Crab arrives at your doorstep, it will already be cooked, glazed and frozen. The king crab will have either been cooked on the fishing boats or immediately upon landfall in order to preserve its fresh, delicious taste. So, you will just need to thaw the legs and reheat the crab before you start enjoying.</p><p> Upon receiving your crab legs, first and foremost, place the crab in your refrigerator to thaw overnight(8 - 12 hours). The next day, it will be ready to reheat and prepare.</p><p> Cooking king crab legs isn’t difficult, and they only takes a few minutes to prepare. The way you choose to cook your king crab will depend on the taste that you want to achieve. Below we’ve listed some of the more popular ways to cook king crab, from boiling to grilling.</p><p> Most of these recipes are very simple — three drops of hot sauce in butter is often all you need to draw out the wonderful taste of fresh king crab!And, most require a reheat time of just 5 - 10 minutes. Much more than that, and you risk your crab becoming overcooked.If you’re adding your crab to a stew or soup, you’ll only need to put it in for the last 5 minutes of the cooking process.</p> ",
                    ShortDescription = "Steamed King Crab with garlic, butter and lemon."
                },
                new Food
                {
                    FoodId = -11,
                    Name = "Spicy Garlic Shrimp",
                    Price = 20,
                    PhotoPath = "a967236d-9257-4bcd-9145-ce28ed7721cf_shrimp.jpg",
                    LongDescription = "<p>When I was living in China, ordering a dish of something coated in spicy garlic sauce was usually a gamble. Take pork or eggplant for example, two Sichuan dishes usually cooked with this sauce. The dish could be either perfectly manageable, or, if the chef was overly generous with his chili oil, throat-burningly spicy. Having your throat burnt isn't necessarily bad, per se (after all, you did go out for Sichuan). But when your meal also includes mapo tofu, dan dan noodles, and boiled beef in chili sauce, it would be nice to have a little reprieve.</p><p> In the U.S., shrimp with garlic sauce in Chinese restaurants is rarely very spicy, even if the entree is marked with a big red star or chili pepper on the menu. I wanted to strike a balance with this recipe.Instead of using a lot of chili oil, like in Sichuan cooking, or a tiny squeeze of hot sauce like many restaurants here seem to do, I used a good amount of Huy Fong(Red Rooster brand) chili garlic sauce instead. (You can use any brand of chili garlic sauce or regular chili sauce.)</p><p> Of course, because this dish is all about the garlicky taste, nothing beats freshly sautéed garlic.There is nothing wrong with adding garlic on top of garlic.You'll want to start with sautéing a few cloves cloves, crushed or chopped. Then add the shrimp and the sauce, and the entree will be done in, seriously, 5 minutes.</p>",
                    ShortDescription = "Zesty, spicy and simple, these tasty shrimp are perfect for a party."
                });

            const string ADMIN_USER_ID = "e554c9c8-0719-4aa2-848a-f0f1eb6182b7";
            const string ADMIN_ROLE_ID = "b213403a-41ab-4295-962c-9239a6e9ae30";
            const string MOD_ROLE_ID = "c9e7d5dc-79a9-49df-a8e5-010196082779";
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = MOD_ROLE_ID,
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                }
                );
            var harsher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = ADMIN_USER_ID,
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = harsher.HashPassword(null, "Admin1234@"),
                    SecurityStamp = string.Empty
                }
                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_USER_ID
                }
                );
        }
    }
}