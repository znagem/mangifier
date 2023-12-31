import 'package:mangifier/models/disease.dart';

class Diagnosis {
  List<Disease> diseases;

  Diagnosis({required this.diseases});

  static Diagnosis fromJson(json) {
    final result = json['result'] as List;

    final diseases = result.map((d) => Disease.fromJson(d)).toList();
    return Diagnosis(diseases: diseases);
  }

  Map<String, dynamic> toJson() {
    final data = <String, dynamic>{};
    data['result'] = diseases.map((disease) => disease.toJson()).toList();
    return data;
  }
}
