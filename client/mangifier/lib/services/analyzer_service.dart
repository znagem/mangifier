import 'dart:convert';

import 'package:mangifier/models/diagnosis.dart';
import 'package:mangifier/services/settings_service.dart';
import 'package:http/http.dart' as http;

class AnalyzerService {
  final _settingsService = SettingsService();

  Future<Diagnosis> analyze(String imagePath, http.Client client) async {
    final url = await _settingsService.get();

    final header = {
      'accept': 'application/json',
      'content-type': 'multipart/form-data',
    };

    final uri = Uri.parse('${url.server}/api/analyze');
    final request = http.MultipartRequest("POST", uri);
    request.headers.addAll(header);

    final image = await http.MultipartFile.fromPath('image', imagePath);
    request.files.add(image);

    final streamResponse = await client.send(request);

    if (streamResponse.statusCode == 200) {
      final response = await http.Response.fromStream(streamResponse);

      final json = jsonDecode(response.body);

      return Diagnosis.fromJson(json);
    } else {
      return Diagnosis(diseases: []);
    }
  }
}
