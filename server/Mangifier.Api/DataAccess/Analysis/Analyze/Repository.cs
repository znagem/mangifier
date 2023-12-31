using Bogus;
using Mangifier.Api.Shared.Analysis;

namespace Mangifier.Api.DataAccess.Analysis.Analyze;

[RepositoryService]
public partial class Repository
{
    //executeAsync
    public async Task<OneOf<DiagnosisDto, ServiceError>> ExecuteAsync(string imageString, CancellationToken ct)
    {
        var diseases = GetMangoDiseases().OrderByDescending(d => d.Score).Take(1).ToArray();
        return await Task.FromResult(new DiagnosisDto { Result = diseases });
    }

    //get mango diseases
    private List<DiseaseDto> GetMangoDiseases()
    {
        var faker = new Faker();

        return
        [
            new DiseaseDto
            {
                Name = "Anthracnose",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The disease causes serious losses to young shoots, flowers and fruits It is also affects fruits during storage.",
                    "The disease produces leaf spot, blossom blight, wither tip, twig blight and fruit rot symptoms. Tender shoots and foliage are easily affected which ultimately cause \"die back\" of young branches. Older twigs may also be infected through wounds which in severe cases may be fatal.",
                    "Depending on the prevailing weather conditions blossom blight may vary in severity from slight to a heavy infection of the panicles. Black spots develop on panicles as well as on fruits. Severe infection destroys the entire inflorescence resulting in no setting of fruits. Young infected fruits develop black spots, shrivel and drop off.",
                    "Fruits infected at mature stage carry the fungus into storage and cause considerable loss during storage, transit and marketing."
                ],
                Preventions =
                [
                    "Sanitation and Pruning: Remove and destroy any infected plant parts, including leaves, twigs, and fruits. Pruning diseased branches helps improve airflow and sunlight penetration, reducing favorable conditions for the fungus.",
                    "Proper Irrigation: Avoid overhead irrigation, which can create a moist environment ideal for fungal growth. Instead, use drip or furrow irrigation to keep the foliage dry.",
                    "Fungicides: Apply fungicides as preventive measures, especially during the flowering and fruiting stages. Copper-based fungicides or those containing active ingredients like azoxystrobin or mancozeb are effective against anthracnose. Follow manufacturer instructions and local regulations when using fungicides."
                ]
            },
            new DiseaseDto
            {
                Name = "Powdery Mildew",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The disease is characterized by the presence of white powdery growth on the leaves, flowers and fruits.",
                    "The affected flowers and fruits drop pre-maturely reducing the crop load considerably or might even prevent the fruit set.",
                    "The fungus parasitizes young tissues of all parts of the inflorescence, leaves and fruits",
                    "Young leaves are attacked on both the sides but it is more conspicuous on the grower surface. Often these patches coalesce and occupy larger areas turning into purplish brown in colour"
                ],
                Preventions =
                [
                    "Pruning and Sanitation: Remove and destroy infected plant parts like leaves, twigs, and fruits. Pruning helps improve air circulation and sunlight penetration, which can inhibit the growth of the powdery mildew fungus.",
                    "Cultural Practices: Proper spacing between trees enhances airflow and reduces humidity around plants. Avoid overhead irrigation to keep foliage dry, as moisture promotes powdery mildew development.",
                    "Fungicides: Apply fungicides preventively or at the first sign of infection. Sulfur-based fungicides or those containing active ingredients like potassium bicarbonate, neem oil, or horticultural oils are effective against powdery mildew. Follow manufacturer instructions and local regulations when using fungicides."
                ]
            },
            new DiseaseDto
            {
                Name = "Bacterial Canker",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The disease is noticed on leaves, leaf stalks, stems, twigs, branches and fruits, initially producing water soaked lesions, later turning into typical canker.",
                    "On leaves, water soaked irregular satellite to angular raised lesions measuring 1-4 mm in diameter are formed. These lesions are light yellow in colour, initially with yellow halo but with age enlarge or coalesce to form irregular necrotic cankerous patches with dark brown colour.",
                    "On fruits, water-soaked, dark brown to black coloured lesions are observed which gradually developed into cankerous, raised or flat spots. These spots grow bigger usually up to 1 to 5 mm in diameter, which covers / almost the whole fruit.",
                    "These spots often, burst extruding gummy substances containing highly contagious bacterial cells."
                ],
                Preventions =
                [
                    "Sanitation: Prune and remove infected plant parts, including branches, twigs, and fruits, and destroy them to prevent the spread of bacteria. Disinfect pruning tools between cuts to avoid further contamination.",
                    "Copper-Based Sprays: Copper-based fungicides or bactericides are commonly used to manage bacterial canker. These sprays can help reduce the spread of the bacteria. Follow manufacturer recommendations and local regulations when applying these products.",
                    "Cultural Practices: Promote overall tree health through proper irrigation, fertilization, and mulching. Well-maintained trees are better equipped to resist diseases, including bacterial canker."
                ]
            },
            new DiseaseDto
            {
                Name = "Die back",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The pathogen causing dieback, tip dieback, graft union blight, twig blight, seedling rot, wood stain, stem-end rot, black root rot, fruit rot, dry rot, brown rot of panicle etc. The disease is most conspicuous during October November.",
                    "It is characterized by drying back of twigs from top downwards, particularly in older trees followed by drying of leaves which gives an appearance of fire scorch. Internal browning in wood tissue is observed when it is slit open along with the long axis.",
                    "Cracks appear on branches and gum exudes before they die out. When graft union of nursery plant is affected, it usually dies"
                ],
                Preventions =
                [
                    "Pruning and Sanitation: Remove and destroy dead, dying, or infected branches to prevent the spread of diseases and to improve the overall health of the tree. Proper pruning also promotes better airflow and sunlight penetration.",
                    "Soil Management: Ensure proper soil drainage and avoid waterlogging, as excessively wet conditions can lead to root rot and contribute to dieback. Mulching and improving soil fertility through proper fertilization can enhance tree vigor.",
                    "Pest and Disease Control: Regularly inspect mango trees for signs of pests and diseases that could contribute to dieback. Implement appropriate measures, such as using pesticides or fungicides, based on the specific pest or disease identified."
                ]
            },
            new DiseaseDto
            {
                Name = "Sooty mould",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The disease is common in the orchards where mealy bug, scale insect and hopper are not controlled efficiently.",
                    "The disease in the field is recognized by the presence of a black velvety coating, i.e., sooty mould on the leaf surface. In severe cases the trees turn completely black due to the presence of mould over the entire surface of twigs and leaves.",
                    "The severity of infection depends on the honey dew secretion by the above said insects. Honey dew secretions from insects sticks to the leaf surface and provide necessary medium for fungal growth."
                ],
                Preventions =
                [
                    "Insect Control: Control the population of honeydew-producing insects by using insecticidal soaps, neem oil, horticultural oils, or other appropriate insecticides. This helps eliminate the honeydew that serves as a food source for the sooty mold.",
                    "Physical Removal: Wash affected plant parts with a gentle stream of water or use a soft brush to physically remove sooty mold. This can be particularly effective for smaller infestations.",
                    "Pruning and Sanitation: Prune and dispose of heavily infested plant parts. This reduces the mold's presence and promotes better airflow and sunlight penetration, which can deter its growth."
                ]
            },
            new DiseaseDto
            {
                Name = "Red rust",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "Red rust disease, caused by an alga, has been observed in mango growing areas. The algal attack causes reduction in photosynthetic activity and defoliation of leaves thereby lowering vitality of the host plant.",
                    "The disease can easily be recognized by the rusty red spots mainly on leaves and sometimes on petioles and bark of young twigs and is epiphytic in nature.",
                    "The spots are greenish grey in colour and velvety in texture. Later, they turn reddish brown. The circular and slightly elevated spots sometimes coalesce to form larger and irregular spots. The disease is more common in closely planted orchards."
                ],
                Preventions =
                [
                    "Fungicidal Treatments: Apply fungicides labeled for use against mango rust. Copper-based fungicides or those containing active ingredients like triazoles or strobilurins can help control the spread of the fungus. Follow manufacturer instructions and local regulations for proper application.",
                    "Pruning and Sanitation: Remove and destroy infected leaves and plant debris to reduce the spread of spores. Proper pruning can also improve air circulation, minimizing favorable conditions for the fungus.",
                    "Cultural Practices: Implement good cultural practices such as proper irrigation, ensuring adequate drainage, maintaining tree health through balanced fertilization, and providing adequate spacing between trees for airflow."
                ]
            },
            new DiseaseDto
            {
                Name = "Scab",
                Score = faker.Random.Double(0.10, 0.95),
                Symptoms =
                [
                    "The scab fungus attack leaves, panicles, blossoms, twigs, bark of stems and mango fruits. Spots are circular, slightly angular, elongated, 2-4 mm in diameter, brown but during rainy season, lesions differ in size, shape and colour.",
                    "Symptoms produced by the disease are very much like those of anthracnose.",
                    "On young fruits, the infection is grey to grayish brown with dark irregular margins. As the fruit attains in size, spots also enlarge and the centre may become covered with the crack fissure and corky tissues."
                ],
                Preventions =
                [
                    "Fungicidal Treatments: Apply fungicides specifically labeled for controlling mango scab. Copper-based fungicides or those containing active ingredients like mancozeb, chlorothalonil, or tebuconazole can help manage the disease. Follow manufacturer instructions and local regulations for proper application.",
                    "Pruning and Sanitation: Prune and remove infected plant parts such as leaves and twigs, and dispose of them properly to prevent the spread of spores. Pruning helps improve air circulation within the canopy, reducing humidity levels that favor fungal growth.",
                    "Cultural Practices: Implement good cultural practices, including proper irrigation techniques (avoiding overhead watering), adequate spacing between trees for airflow, and balanced fertilization to maintain tree health."
                ]
            }
        ];
    }
}