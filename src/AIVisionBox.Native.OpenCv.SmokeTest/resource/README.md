# AIVisionBox.Native.OpenCv.SmokeTest

Native DLL が 単体で正しく動作するか を確認するためのテスト用コンソールアプリです。

---

目的：

- DLL の export / link / runtime DLL 配置確認
- OpenCV 処理の実画像検証
- C# や Web を介さず、問題の切り分けを容易にする

SmokeTest では以下を実施します。

- assets/opencv/cards.png を読み込み
- ダイヤ（赤いマーク）の個数をカウント
- 結果を標準出力に表示

---

## 使用している OpenCV

- OpenCV 4.x（Windows prebuilt）
- opencv_world4xxx.dll を使用
- Debug / Release で DLL を切り替える構成

---

## 今後の展開

- C# P/Invoke による IAiInferenceEngine 実装
- CMake 化（Linux / RZ/V / クロスビルド対応）
- AI 推論前後の前処理・後処理としての活用

---

## 注意事項

- 本プロジェクトは現在 検証・基盤構築フェーズ
- API は将来変更される可能性があります
- 商用利用時は OpenCV ライセンス条件を確認してください

