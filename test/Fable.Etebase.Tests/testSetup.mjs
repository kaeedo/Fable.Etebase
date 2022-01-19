import { Account } from "etebase";
//import TestData from "./testData.mjs";

const username = "JessicaHyde";
const password = "WhereAmI";
const email = "jessicahyde@example.com";

const username2 = "WilsonWilson";
const password2 = "Mr. Rabbit";
const email2 = "wilsonwilson@example.com";

const server = "http://172.18.122.191:3735";

const TestData = {
  user1: { username, password, email },
  user2: { username: username2, password: password2, email: email2 },
  server: server,
};

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
