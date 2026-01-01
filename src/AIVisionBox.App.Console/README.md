# AIVisionBox.App.Console

AIVisionBox の **最小ホストアプリケーション（Console版）** です。

UIを持たず、設定読み込み・推論実行・結果表示を目的としています。

---

## 役割

- `appsettings.json` の読み込み
- 推論エンジンの生成・初期化
- 推論処理の実行と結果出力

---

## このプロジェクトの位置づけ

- 開発初期の **動作確認・検証用**
- UnitTest ではカバーしきれない **実行フローの確認**
- Blazor / Web アプリケーションの **前段階**

---

## 特徴

- DI を使用せず、明示的な Engine 生成（理解しやすさ優先）
- FakeEngine / HttpInferenceEngine の切り替えが可能
- 実機が無くても実行可能

---

## 今後の展開

- Blazor Server App への移植
- 実推論サーバ（RZ/V 等）との接続
- 設定項目の拡張