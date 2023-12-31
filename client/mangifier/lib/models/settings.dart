class Settings {
  String server;

  Settings({required this.server});

  factory Settings.fromJson(Map<String, dynamic> json) {
    return Settings(
      server: json['server'] as String,
    );
  }

  Map<String, dynamic> toJson() {
    return {"server": server};
  }
}
