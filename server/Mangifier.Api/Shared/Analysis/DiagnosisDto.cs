namespace Mangifier.Api.Shared.Analysis;

public sealed class DiagnosisDto
{
    public DiseaseDto[] Result { get; set; } = Array.Empty<DiseaseDto>();
}