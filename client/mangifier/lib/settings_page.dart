import 'package:flutter/material.dart';
import 'package:mangifier/base.dart';
import 'package:mangifier/models/settings.dart';
import 'package:mangifier/services/settings_service.dart';
import 'package:mangifier/toast.dart';

class SettingsPage extends StatefulWidget {
  const SettingsPage({super.key});

  @override
  State<SettingsPage> createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {
  late TextEditingController _serverUrlController;
  late SettingsService _settingsService;

  @override
  void initState() {
    super.initState();

    _serverUrlController = TextEditingController();
    _settingsService = SettingsService();
  }

  @override
  void dispose() {
    _serverUrlController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return FutureBuilder<void>(
        future: initialize(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Base(
              appBar: AppBar(
                title: const Text("Settings"),
                centerTitle: true,
                leading: IconButton(
                  onPressed: () => Navigator.pop(context),
                  icon: const Icon(
                    Icons.chevron_left,
                    color: Colors.white,
                  ),
                ),
              ),
              child: const Expanded(
                child: Center(
                  child: CircularProgressIndicator(
                      strokeCap: StrokeCap.round, strokeWidth: 5),
                ),
              ),
            );
          } else {
            return Base(
              appBar: AppBar(
                // ignore: prefer_const_constructors
                title: Text("Settings"),
                centerTitle: true,
                leading: IconButton(
                  onPressed: () => Navigator.pop(context),
                  icon: const Icon(
                    Icons.chevron_left,
                    color: Colors.white,
                  ),
                ),
              ),
              child: SingleChildScrollView(
                child: Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: Column(
                    children: [
                      Card(
                        child: Padding(
                          padding: const EdgeInsets.fromLTRB(24, 12, 24, 12),
                          child: Column(
                            children: [
                              Align(
                                  alignment: Alignment.centerLeft,
                                  child: Text(
                                    "Server URL",
                                    style: theme.textTheme.titleMedium,
                                  )),
                              TextField(
                                decoration: const InputDecoration(
                                  hintText: "http://localhost:5000",
                                ),
                                controller: _serverUrlController,
                              ),
                              const SizedBox(height: 20),
                              Align(
                                alignment: Alignment.centerRight,
                                child: ElevatedButton(
                                  onPressed: onServerConnectionSave,
                                  child: const Text("SAVE"),
                                ),
                              )
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            );
          }
        });
  }

  Future<void> onServerConnectionSave() async {
    await _settingsService.save(Settings(
      server: _serverUrlController.text,
    ));

    Toast.showSuccess("Server connection saved");
  }

  Future<void>? initialize() async {
    final settings = await _settingsService.get();
    _serverUrlController.text = settings.server;
  }
}
