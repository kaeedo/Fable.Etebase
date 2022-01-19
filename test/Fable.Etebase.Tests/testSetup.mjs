import { Account } from "etebase";
import TestData from "./testData.mjs";

async function check(username, email, password) {
  try {
    await Account.login(username, password, TestData.server);
  } catch {
    await Account.signup({ username, email }, password, TestData.server);
  }
}

await check(
  TestData.user1.username,
  TestData.user1.email,
  TestData.user1.password
);
await check(
  TestData.user2.username,
  TestData.user2.email,
  TestData.user2.password
);
