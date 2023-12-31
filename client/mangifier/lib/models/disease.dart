class Disease {
  double score;
  String name;
  List<String> symptoms;
  List<String> preventions;

  Disease({
    required this.score,
    required this.name,
    required this.symptoms,
    required this.preventions,
  });

  factory Disease.fromJson(Map<String, dynamic> json) {
    final score = json['score'] as double;
    final name = json['name'] as String;
    final symptomsRaw = json['symptoms'] as List;
    final symptoms = symptomsRaw.map((s) => s as String).toList();
    final preventionsRaw = json['preventions'] as List;
    final preventions = preventionsRaw.map((p) => p as String).toList();

    return Disease(
      score: score,
      name: name,
      symptoms: symptoms,
      preventions: preventions,
    );
  }

  Map<String, dynamic> toJson() {
    final data = <String, dynamic>{};
    data['score'] = score;
    data['name'] = name;
    data['symptoms'] = symptoms;
    data['preventions'] = preventions;
    return data;
  }
}
