# AIVisionBox.Core.Tests

AIVisionBox.Core の **契約テスト（UnitTest）** をまとめたプロジェクトです。

---

## 役割

- Core 層の API 契約が壊れていないことを保証
- データモデルの基本動作確認
- 上位層（Engine / App）の安心材料

---

## 方針

- 外部依存を一切持たない
- 実装の正しさではなく「契約の成立」を確認
- テストは軽量・高速を重視

