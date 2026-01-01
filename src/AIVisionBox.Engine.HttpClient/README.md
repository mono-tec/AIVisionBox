# AIVisionBox.Engine.HttpClient

HTTP 経由で **外部AI推論サーバ** と通信する推論エンジン実装です。

Renesas RZ/V シリーズを含む、  
「HTTP API を持つ AI 推論BOX」との接続を想定しています。

---

## 役割

- `IAiInferenceEngine` の HTTP 実装
- 画像データを HTTP リクエストに変換して送信
- 推論結果を `InferenceResult` にマッピング

---

## 特徴

- **AI実機が無くても開発・テスト可能**
- `HttpClient` を注入可能な設計（UnitTest対応）
- 通信仕様は暫定DTOとして定義し、後から実機仕様に合わせて調整可能

---

## 含まれる主な要素

- `HttpInferenceEngine`  
  HTTP 推論エンジン本体

- `HttpInferenceSettings`  
  接続先URL・タイムアウト・モデル名などの設定

- `Dtos`  
  推論リクエスト／レスポンス用 DTO（暫定仕様）

---

## 設計方針

- ネットワーク通信は `HttpClient` に集約
- エンジン内部で `HttpClient` のライフサイクルは管理しない
- Core への依存は **参照のみ**

---

## 補足

- v0.x では「静止画1枚」を前提
- 実通信処理は段階的に実装予定

