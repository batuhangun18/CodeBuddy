export type SubscriptionType = "Free" | "Pro";

export type AuthResponse = {
  userId: string;
  userName: string;
  email: string;
  subscriptionType: SubscriptionType;
  token: string;
};

export type UserProfile = {
  id: string;
  userName: string;
  email: string;
  subscriptionType: SubscriptionType;
  avatarUrl?: string;
  displayColor?: string;
  createdAt: string;
};
