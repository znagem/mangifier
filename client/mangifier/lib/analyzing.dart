import 'package:avatar_glow/avatar_glow.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:lottie/lottie.dart';
import 'package:mangifier/base.dart';

class Analyzing extends StatefulWidget {
  const Analyzing({super.key, required this.imagePath, required this.client});

  final String imagePath;
  final http.Client client;

  @override
  State<Analyzing> createState() => _AnalyzingState();
}

class _AnalyzingState extends State<Analyzing> {
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final accent = theme.colorScheme.secondary;

    return Base(
      child: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            AvatarGlow(
              glowColor: theme.primaryColor,
              glowShape: BoxShape.circle,
              animate: true,
              glowRadiusFactor: 0.10,
              curve: Curves.fastOutSlowIn,
              child: SizedBox(
                width: 150,
                height: 150,
                child: ClipOval(
                  child: Transform.scale(
                    scale: 1 / 0.7,
                    child: Lottie.asset('assets/images/loading.json',
                        fit: BoxFit.fill, repeat: true),
                  ),
                ),
              ),
            ),
            const SizedBox(height: 20),
            Text(
              "Analyzing...",
              style: theme.textTheme.bodyLarge,
            ),
            const SizedBox(height: 20),
            ElevatedButton.icon(
              onPressed: () {
                widget.client.close();
                Navigator.pop(context);
              },
              style: ButtonStyle(
                  foregroundColor:
                      MaterialStateProperty.all<Color>(Colors.white),
                  backgroundColor: MaterialStateProperty.all<Color>(accent),
                  shape: MaterialStateProperty.all<RoundedRectangleBorder>(
                      RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20.0))),
                  minimumSize:
                      MaterialStateProperty.all<Size>(const Size(120, 50))),
              icon: const Icon(Icons.stop_rounded),
              label: const Text(
                "Cancel",
                style: TextStyle(
                    color: Colors.white,
                    fontSize: 16,
                    fontWeight: FontWeight.w500),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
