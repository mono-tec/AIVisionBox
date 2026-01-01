# AIVisionBox.Engine.HttpClient.Tests

HTTP 推論エンジンの **UnitTest** プロジェクトです。

---

## 役割

- 実ネットワークに出ない HTTP 通信テスト
- 設定反映・例外処理・契約確認
- AI実機が無くても品質を担保する

---

## 特徴

- `FakeHttpMessageHandler` による完全な通信偽装
- `HttpClient` 注入設計の検証
- 実通信実装前からテストが回る構成

---

## 重要な考え方

- AIモデルの正しさはテストしない
- 通信・変換・エラー処理を重点的に確認する

