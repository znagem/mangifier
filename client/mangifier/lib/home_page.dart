import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:image_picker/image_picker.dart';
import 'package:lottie/lottie.dart';
import 'package:mangifier/analysis_page.dart';
import 'package:mangifier/info_page.dart';
import 'package:mangifier/settings_page.dart';
import 'package:mangifier/toast.dart';
import 'package:mangifier/user_page.dart';

typedef FunctionStringCallback = void Function(String);

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> with TickerProviderStateMixin {
  late final AnimationController _controller;

  @override
  void initState() {
    super.initState();
    ToastContext().init(context);
    _controller = AnimationController(vsync: this);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final icons = {
      TargetPlatform.android: const IconState(
          icon: 'assets/icons/scan.svg', tooltip: 'Capture image'),
      TargetPlatform.iOS: const IconState(
          icon: 'assets/icons/scan.svg', tooltip: 'Capture image'),
      TargetPlatform.linux: const IconState(
          icon: 'assets/icons/photo.svg',
          tooltip: 'Select image',
          source: ImageSource.gallery),
      TargetPlatform.macOS: const IconState(
          icon: 'assets/icons/photo.svg',
          tooltip: 'Select image',
          source: ImageSource.gallery),
      TargetPlatform.windows: const IconState(
          icon: 'assets/icons/photo.svg',
          tooltip: 'Select image',
          source: ImageSource.gallery),
      TargetPlatform.fuchsia: const IconState(
          icon: 'assets/icons/photo.svg',
          tooltip: 'Select image',
          source: ImageSource.gallery),
    };

    final iconState = icons[defaultTargetPlatform] ??
        const IconState(
            icon: 'assets/icons/scan.svg', tooltip: 'Capture image');

    return Stack(
      fit: StackFit.loose,
      alignment: Alignment.bottomCenter,
      children: [
        Center(
          child: SizedBox(
            width: 220,
            height: 220,
            child: ClipOval(
              child: Transform.scale(
                scale: 1 / 0.7,
                child: Lottie.asset(
                  'assets/images/loading.json',
                  filterQuality: FilterQuality.high,
                  controller: _controller,
                  onLoaded: (composition) {
                    _controller
                      ..duration = composition.duration
                      ..forward();
                  },
                ),
              ),
            ),
          ),
        ),
        Container(
          margin: const EdgeInsets.all(20),
          child: ClipRRect(
            borderRadius: const BorderRadius.all(Radius.circular(15)),
            child: Container(
              decoration: const BoxDecoration(
                borderRadius: BorderRadius.all(Radius.circular(15)),
                color: Color(0xFFF06C34),
              ),
              padding: const EdgeInsets.fromLTRB(12, 8, 12, 8),
              child: Row(mainAxisSize: MainAxisSize.min, children: [
                IconButton(
                  tooltip: 'App info',
                  iconSize: 24,
                  icon: const Icon(
                    Icons.info_outline_rounded,
                    color: Colors.white,
                  ),
                  onPressed: () => Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const InfoPage(),
                    ),
                  ),
                ),
                if (_isMobile) SizedBox(width: _isMobile ? 10 : 20),
                if (_isMobile)
                  IconButton(
                    tooltip: "Select image",
                    iconSize: 24,
                    icon: SvgPicture.asset(
                      "assets/icons/photo.svg",
                      colorFilter:
                          const ColorFilter.mode(Colors.white, BlendMode.srcIn),
                    ),
                    onPressed: () => pickImage(ImageSource.gallery),
                  ),
                SizedBox(width: _isMobile ? 10 : 20),
                IconButton(
                  tooltip: iconState.tooltip,
                  icon: SvgPicture.asset(
                    iconState.icon,
                    colorFilter:
                        const ColorFilter.mode(Colors.white, BlendMode.srcIn),
                  ),
                  onPressed: () => pickImage(iconState.source),
                ),
                SizedBox(width: _isMobile ? 10 : 20),
                IconButton(
                  tooltip: 'User Info',
                  iconSize: 24,
                  icon: const Icon(
                    Icons.account_circle,
                    color: Colors.white,
                  ),
                  onPressed: () => Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const UserPage(),
                    ),
                  ),
                ),
                SizedBox(width: _isMobile ? 10 : 20),
                IconButton(
                  tooltip: 'Settings',
                  iconSize: 24,
                  icon: const Icon(
                    Icons.settings_outlined,
                    color: Colors.white,
                  ),
                  onPressed: () => Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const SettingsPage(),
                    ),
                  ),
                ),
              ]),
            ),
          ),
        )
      ],
    );
  }

  bool get _isMobile {
    return defaultTargetPlatform == TargetPlatform.android ||
        defaultTargetPlatform == TargetPlatform.iOS;
  }

  Future<void> pickImage(ImageSource source) async {
    final picker = ImagePicker();
    final image = await picker.pickImage(source: source);

    if (image == null) return;

    if (context.mounted) showResult(image.path);
  }

  void showResult(String result) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => AnalysisPage(imagePath: result),
      ),
    );
  }
}

class IconState {
  final String icon;
  final String tooltip;
  final ImageSource source;

  const IconState(
      {required this.icon,
      required this.tooltip,
      this.source = ImageSource.camera});
}
