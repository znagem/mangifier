import 'package:flutter/material.dart';

class Base extends StatelessWidget {
  const Base({super.key, this.appBar, required this.child});

  final Widget? appBar;
  final Widget child;

  @override
  Widget build(BuildContext context) {
    final header = appBar ?? const SizedBox();
    return Container(
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
            stops: [
              0.0,
              0.5,
            ],
            colors: [
              Color(0xFF1D8121),
              Color(0xFFFFFFFF),
            ],
          ),
        ),
        child: Column(
          children: [header, Expanded(child: child)],
        ));
  }
}
