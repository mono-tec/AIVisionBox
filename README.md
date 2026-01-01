# AIVisionBox

AIVisionBox は、**AI × 画像処理 × 制御システム**を前提とした  
**拡張可能な推論基盤（アプリケーションフレームワーク）**です。

本リポジトリは、

- 実機 AI チップが無い状態でも開発を進められる
- 推論エンジンを差し替え可能な構成を持つ
- 産業用途（FA / 制御 / 画像検査）を想定した設計

を目的として構築されています。

---

## コンセプト

AIVisionBox は「AIモデルを作る」ことを目的としていません。

代わりに、

- **既存の AI 推論技術**
- **画像処理（OpenCV 等）**
- **PLC・制御・通信技術**

を **現場で使える形に統合する基盤**を提供します。

> AI は“作るもの”ではなく、“組み込むもの”

という立ち位置を明確にしています。

---

## 全体構成

```
AIVisionBox
├─ AIVisionBox.Core
├─ AIVisionBox.Engine.HttpClient
├─ AIVisionBox.Engine.OpenCv       （予定）
├─ AIVisionBox.Mock.InferenceServer
├─ AIVisionBox.Mock.InferenceServer.Tests
├─ AIVisionBox.App.ConsoleApp
└─ docs
```

---

## 現在のステータス

- Core（契約モデル）：安定
- HttpInferenceEngine：実装済み
- Mock Inference Server：実装済み
- UnitTest：Core / Engine / Server
- OpenCV Engine（C++/CLI）：計画中
- Blazor UI：計画中

---

## 特徴

- 推論エンジン差し替え設計
- 実機なしでの先行開発
- 産業用途を前提とした堅牢設計

---

## クイックスタート（Mock 推論）

1. Mock サーバ起動  
   ```
   dotnet run --project AIVisionBox.Mock.InferenceServer
   ```

2. Console アプリ起動  
   ```
   dotnet run --project AIVisionBox.App.ConsoleApp
   ```

---

## ライセンス

MIT © 2026 mono-tec

---

## 注意事項

本プロジェクトは学習・検証用途を主目的としています。
