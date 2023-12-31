namespace Mangifier.Api.Shared.Analysis;

public sealed class DiseaseDto
{
    public double Score { get; set; }
    public string Name { get; set; } = string.Empty;

    public string[] Symptoms { get; set; } = Array.Empty<string>();
    public string[] Preventions { get; set; } = Array.Empty<string>();
}