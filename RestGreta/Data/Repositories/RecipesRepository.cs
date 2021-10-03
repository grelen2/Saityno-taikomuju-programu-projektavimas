using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestGreta.Data.Entities;

namespace RestGreta.Data.Repositories
{
    public interface IRecipesRepository
    {
        Task<Recipe> Create(Recipe recipe);
        Task Delete(Recipe recipe);
        Task<Recipe> Get(int id);
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Put(Recipe recipe);
    }

    public class RecipesRepository : IRecipesRepository
    {
        public async Task<IEnumerable<Recipe>> GetAll()
        {
            return new List<Recipe>
            {
                new Recipe()
                {
                    RecipeName = "Obuoliu pyragas",
                    ProductName = "Kiausiniai, sviestas, kvietiniai miltai, cukrus, obuoliai, kepimo milteliai, vanilinis cukrus",
                    Quantity = "4, 200, 250, 180, 5, 2, 2", 
                    SeesUnits = "vnt, g, g, g, vnt, sauksteliai, sauksteliai",
                    Description =   "1.	Orkaitę įjunkite kaisti iki 180 C laipsnių (kaitinimo režimu viršus+apačia, be ventiliatoriaus)./n" +
                                    "2.  Sviestą ištirpinkite ir palikite atvėsti./n" +
                                    "3.  Obuolius nulupkite, išpjaukite sėklalizdžius ir supjaustykite plonomis skiltelėmis (kad obuoliai neparuduotų, galite pašlakstyti trupučiu citrinos sulčių)./n" +
                                    "4.  Kiaušinius gerai išplakite su cukrumi ir vaniliniu cukrumi iki masė pabals ir jos tūris padidės daugmaž 2-3 kartus. Supilkite atvėsusį sviestą bei dar truputį paplakite./m" +
                                    "5.  Miltus sumaišykite su kepimo milteliais. Šį mišinį per kelis kartus įsijokite į kiaušinių plakinį kaskart švelniais judesiais (šaukštu arba mentele, ne plaktuvu) išmaišydami, jog neliktų sausų miltų. Tešla turėtų gautis daugmaž riebios grietinės tirštumo./n" +
                                    "6.  Į tešlą sudėkite obuolius ir atsargiai išmaišykite, stengdamiesi per daug tešlos nepermaišyti, nes tada pyragas gali prasčiau kilti./n" +
                                    "7.  Kepimo formos (naudojau apvalią 24 cm skersmens) dugną išklokite kepimo popieriumi. Sukrėskite tešlą./n" +
                                    "8.  Pyragą šaukite į iki 180 C laipsnių įkaitusios orkaitės vidurį ir kepkite apie 35-45 minutes, iki viršus bus gražiai auksinės spalvos. Ar pyragas iškepė, galite patikrinti įkišę medinį pagaliuką į pyrago centrą - jeigu ištrauktas jis bus neapsivėlęs tešla, vadinasi pyragas iškepęs, jei ne - pakepkite dar 5-7 minutes ir vėl patikrinkite./n" +
                                    "9.  Traukite pyragą iš orkaitės, leiskite jam truputį atvėsti, o tada jau gardžiuokitės!",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new Recipe()
                {
                    RecipeName = "Obuoliu pyragas",
                    ProductName = "Kiausiniai, sviestas, kvietiniai miltai, cukrus, obuoliai, kepimo milteliai, vanilinis cukrus",
                    Quantity = "4, 200, 250, 180, 5, 2, 2",
                    SeesUnits = "vnt, g, g, g, vnt, sauksteliai, sauksteliai",
                    Description =   "1.	Orkaitę įjunkite kaisti iki 180 C laipsnių (kaitinimo režimu viršus+apačia, be ventiliatoriaus)./n" +
                                    "2.  Sviestą ištirpinkite ir palikite atvėsti./n" +
                                    "3.  Obuolius nulupkite, išpjaukite sėklalizdžius ir supjaustykite plonomis skiltelėmis (kad obuoliai neparuduotų, galite pašlakstyti trupučiu citrinos sulčių)./n" +
                                    "4.  Kiaušinius gerai išplakite su cukrumi ir vaniliniu cukrumi iki masė pabals ir jos tūris padidės daugmaž 2-3 kartus. Supilkite atvėsusį sviestą bei dar truputį paplakite./m" +
                                    "5.  Miltus sumaišykite su kepimo milteliais. Šį mišinį per kelis kartus įsijokite į kiaušinių plakinį kaskart švelniais judesiais (šaukštu arba mentele, ne plaktuvu) išmaišydami, jog neliktų sausų miltų. Tešla turėtų gautis daugmaž riebios grietinės tirštumo./n" +
                                    "6.  Į tešlą sudėkite obuolius ir atsargiai išmaišykite, stengdamiesi per daug tešlos nepermaišyti, nes tada pyragas gali prasčiau kilti./n" +
                                    "7.  Kepimo formos (naudojau apvalią 24 cm skersmens) dugną išklokite kepimo popieriumi. Sukrėskite tešlą./n" +
                                    "8.  Pyragą šaukite į iki 180 C laipsnių įkaitusios orkaitės vidurį ir kepkite apie 35-45 minutes, iki viršus bus gražiai auksinės spalvos. Ar pyragas iškepė, galite patikrinti įkišę medinį pagaliuką į pyrago centrą - jeigu ištrauktas jis bus neapsivėlęs tešla, vadinasi pyragas iškepęs, jei ne - pakepkite dar 5-7 minutes ir vėl patikrinkite./n" +
                                    "9.  Traukite pyragą iš orkaitės, leiskite jam truputį atvėsti, o tada jau gardžiuokitės!",
                    CreationTimeUtc = DateTime.UtcNow
                }
            };
        }
        public async Task<Recipe> Get(int id)
        {
            return new Recipe()
            {
                RecipeName = "Obuoliu pyragas",
                ProductName = "Kiausiniai, sviestas, kvietiniai miltai, cukrus, obuoliai, kepimo milteliai, vanilinis cukrus",
                Quantity = "4, 200, 250, 180, 5, 2, 2",
                SeesUnits = "vnt, g, g, g, vnt, sauksteliai, sauksteliai",
                Description = "1.	Orkaitę įjunkite kaisti iki 180 C laipsnių (kaitinimo režimu viršus+apačia, be ventiliatoriaus)./n" +
                                    "2.  Sviestą ištirpinkite ir palikite atvėsti./n" +
                                    "3.  Obuolius nulupkite, išpjaukite sėklalizdžius ir supjaustykite plonomis skiltelėmis (kad obuoliai neparuduotų, galite pašlakstyti trupučiu citrinos sulčių)./n" +
                                    "4.  Kiaušinius gerai išplakite su cukrumi ir vaniliniu cukrumi iki masė pabals ir jos tūris padidės daugmaž 2-3 kartus. Supilkite atvėsusį sviestą bei dar truputį paplakite./m" +
                                    "5.  Miltus sumaišykite su kepimo milteliais. Šį mišinį per kelis kartus įsijokite į kiaušinių plakinį kaskart švelniais judesiais (šaukštu arba mentele, ne plaktuvu) išmaišydami, jog neliktų sausų miltų. Tešla turėtų gautis daugmaž riebios grietinės tirštumo./n" +
                                    "6.  Į tešlą sudėkite obuolius ir atsargiai išmaišykite, stengdamiesi per daug tešlos nepermaišyti, nes tada pyragas gali prasčiau kilti./n" +
                                    "7.  Kepimo formos (naudojau apvalią 24 cm skersmens) dugną išklokite kepimo popieriumi. Sukrėskite tešlą./n" +
                                    "8.  Pyragą šaukite į iki 180 C laipsnių įkaitusios orkaitės vidurį ir kepkite apie 35-45 minutes, iki viršus bus gražiai auksinės spalvos. Ar pyragas iškepė, galite patikrinti įkišę medinį pagaliuką į pyrago centrą - jeigu ištrauktas jis bus neapsivėlęs tešla, vadinasi pyragas iškepęs, jei ne - pakepkite dar 5-7 minutes ir vėl patikrinkite./n" +
                                    "9.  Traukite pyragą iš orkaitės, leiskite jam truputį atvėsti, o tada jau gardžiuokitės!",
                CreationTimeUtc = DateTime.UtcNow
            };

        }
        public async Task<Recipe> Create(Recipe recipe)
        {
            return new Recipe()
            {
                RecipeName = "Obuoliu pyragas",
                ProductName = "Kiausiniai, sviestas, kvietiniai miltai, cukrus, obuoliai, kepimo milteliai, vanilinis cukrus",
                Quantity = "4, 200, 250, 180, 5, 2, 2",
                SeesUnits = "vnt, g, g, g, vnt, sauksteliai, sauksteliai",
                Description = "1.	Orkaitę įjunkite kaisti iki 180 C laipsnių (kaitinimo režimu viršus+apačia, be ventiliatoriaus)./n" +
                                    "2.  Sviestą ištirpinkite ir palikite atvėsti./n" +
                                    "3.  Obuolius nulupkite, išpjaukite sėklalizdžius ir supjaustykite plonomis skiltelėmis (kad obuoliai neparuduotų, galite pašlakstyti trupučiu citrinos sulčių)./n" +
                                    "4.  Kiaušinius gerai išplakite su cukrumi ir vaniliniu cukrumi iki masė pabals ir jos tūris padidės daugmaž 2-3 kartus. Supilkite atvėsusį sviestą bei dar truputį paplakite./m" +
                                    "5.  Miltus sumaišykite su kepimo milteliais. Šį mišinį per kelis kartus įsijokite į kiaušinių plakinį kaskart švelniais judesiais (šaukštu arba mentele, ne plaktuvu) išmaišydami, jog neliktų sausų miltų. Tešla turėtų gautis daugmaž riebios grietinės tirštumo./n" +
                                    "6.  Į tešlą sudėkite obuolius ir atsargiai išmaišykite, stengdamiesi per daug tešlos nepermaišyti, nes tada pyragas gali prasčiau kilti./n" +
                                    "7.  Kepimo formos (naudojau apvalią 24 cm skersmens) dugną išklokite kepimo popieriumi. Sukrėskite tešlą./n" +
                                    "8.  Pyragą šaukite į iki 180 C laipsnių įkaitusios orkaitės vidurį ir kepkite apie 35-45 minutes, iki viršus bus gražiai auksinės spalvos. Ar pyragas iškepė, galite patikrinti įkišę medinį pagaliuką į pyrago centrą - jeigu ištrauktas jis bus neapsivėlęs tešla, vadinasi pyragas iškepęs, jei ne - pakepkite dar 5-7 minutes ir vėl patikrinkite./n" +
                                    "9.  Traukite pyragą iš orkaitės, leiskite jam truputį atvėsti, o tada jau gardžiuokitės!",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task<Recipe> Put(Recipe recipe)
        {
            return new Recipe()
            {
                RecipeName = "Obuoliu pyragas",
                ProductName = "Kiausiniai, sviestas, kvietiniai miltai, cukrus, obuoliai, kepimo milteliai, vanilinis cukrus",
                Quantity = "4, 200, 250, 180, 5, 2, 2",
                SeesUnits = "vnt, g, g, g, vnt, sauksteliai, sauksteliai",
                Description = "1.	Orkaitę įjunkite kaisti iki 180 C laipsnių (kaitinimo režimu viršus+apačia, be ventiliatoriaus)./n" +
                                    "2.  Sviestą ištirpinkite ir palikite atvėsti./n" +
                                    "3.  Obuolius nulupkite, išpjaukite sėklalizdžius ir supjaustykite plonomis skiltelėmis (kad obuoliai neparuduotų, galite pašlakstyti trupučiu citrinos sulčių)./n" +
                                    "4.  Kiaušinius gerai išplakite su cukrumi ir vaniliniu cukrumi iki masė pabals ir jos tūris padidės daugmaž 2-3 kartus. Supilkite atvėsusį sviestą bei dar truputį paplakite./m" +
                                    "5.  Miltus sumaišykite su kepimo milteliais. Šį mišinį per kelis kartus įsijokite į kiaušinių plakinį kaskart švelniais judesiais (šaukštu arba mentele, ne plaktuvu) išmaišydami, jog neliktų sausų miltų. Tešla turėtų gautis daugmaž riebios grietinės tirštumo./n" +
                                    "6.  Į tešlą sudėkite obuolius ir atsargiai išmaišykite, stengdamiesi per daug tešlos nepermaišyti, nes tada pyragas gali prasčiau kilti./n" +
                                    "7.  Kepimo formos (naudojau apvalią 24 cm skersmens) dugną išklokite kepimo popieriumi. Sukrėskite tešlą./n" +
                                    "8.  Pyragą šaukite į iki 180 C laipsnių įkaitusios orkaitės vidurį ir kepkite apie 35-45 minutes, iki viršus bus gražiai auksinės spalvos. Ar pyragas iškepė, galite patikrinti įkišę medinį pagaliuką į pyrago centrą - jeigu ištrauktas jis bus neapsivėlęs tešla, vadinasi pyragas iškepęs, jei ne - pakepkite dar 5-7 minutes ir vėl patikrinkite./n" +
                                    "9.  Traukite pyragą iš orkaitės, leiskite jam truputį atvėsti, o tada jau gardžiuokitės!",
                CreationTimeUtc = DateTime.UtcNow
            };
        }
        public async Task Delete(Recipe recipe)
        {
        }
    }
}
